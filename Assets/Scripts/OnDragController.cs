using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDragController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //Радиус, на который будет двигаться джойстик
    public float Range = 10;
    //Название джойстика
    public string StickName;
    //место, где появился джойстик(чтоб считать радиус
    Vector2 _startPosition;
    //делаем локальную переменную для нашего джойстика
    //чтоб постоянно не обращаться к нему через
    //transform.GetChild(0);
    Transform _joystickTransform;
    void Start()
    {
        //В начале работы необходимо зарегестрировать джойстик
        AndroidIosInput.RegisterJoystick(StickName);
        //Запоминаем изначальное положение джойстика

        //_startPosition = transform.GetChild(0).position;
        
        //Запоминаем джойстик в локальной переменной
        _joystickTransform = transform.GetChild(0);
    }
    //Метод, который срабатывает, когда объект пытаются двигать
    public void OnDrag(PointerEventData Data)
    {
        // переопределил _startPosition, т.к. изначально он запоминал точку, которую оставлял и после поворота экрана, из-за этого не верно работал
        _startPosition = transform.position;

        //Ппроверяем расстояние до точки, куда джойстик пытаются перетащить
        if (Vector2.Distance(_startPosition, Data.position) < Range)
        {
            //Если расстояние приемлимое, то переносим наш джойстик туда, где сейчас палец
            _joystickTransform.position = Data.position;
            //и заносим данные о джойстике в наш класс
            //где (Data.position.x - _startPosition.x) / Range - значение от -1 до 1
            AndroidIosInput.SetJoystickValue(StickName, new Vector2((Data.position.x - _startPosition.x) / Range, (Data.position.y - _startPosition.y) / Range));
        }
        else
        {
            //запоминаем смещение по x и y от начала
            float deltaX = Data.position.x - _startPosition.x;
            float deltaY = Data.position.y - _startPosition.y;
            //Ищем точку на тригонометрической окружности относительно смещения
            //И чтоб была на краю нашего Range
            Vector2 state = Vector2.ClampMagnitude(new Vector2(deltaX, deltaY), Range);
            //перемещаем джойстик в эту точку
            _joystickTransform.position = state + _startPosition;
            //Устанавливаем значение джойстика
            //где state.x / Range - значение от -1 до 1
            AndroidIosInput.SetJoystickValue(StickName, new Vector2(state.x / Range, state.y / Range));
        }
    }
    public void OnEndDrag(PointerEventData Data)
    {
        //Обнуляем наш джойстик
        AndroidIosInput.SetJoystickValue(StickName, Vector2.zero);
        //Возвращаем его на прежнее место
        _joystickTransform.position = _startPosition;
    }
    private void OnDestroy()
    {
        //Удаляем запись о джойстике в случае смены сцены или удаления джойстика
        AndroidIosInput.RemoveJoystick(StickName);
    }
}

//Делаем класс статическим, чтобы его элементы были доступны из любого места
public static class AndroidIosInput
{
    //Словарь для того, чтоб хранить данные о джойстике по его имени
    static Dictionary<string, Vector2> JoySticks;
 
    static AndroidIosInput()
    {
        //при загрузке этого класса, мы создаем новый словарь
        JoySticks = new Dictionary<string, Vector2> ();
    }
    //Метод регистрации. Когда джойстик появляется, он регистрируется
    public static void RegisterJoystick(string Name)
    {
        //Проверяем этот джойстик на наличие
        if (JoySticks.ContainsKey(Name))
            throw new System.Exception("Joystick " + Name + " already registered");
        //Если его нет, то регистрируем наш джойстик
        JoySticks.Add(Name, Vector2.zero);
    }
    //Получение значения джойстика
    public static Vector2 GetJoystickValue(string Name)
    {
        //Проверяем его наличие
        if (!JoySticks.ContainsKey(Name))
            throw new System.Exception("Joystick " + Name + " didn't registered");
        //Возвращаем значение джойстика, если он есть
        return JoySticks[Name];
    }

    //Установление значения джойстика
    public static void SetJoystickValue(string Name, Vector2 Value)
    {
        if (!JoySticks.ContainsKey(Name))
            throw new System.Exception("Joystick " + Name + " didn't registered");
        //Устанавливаем значение для джойстика
        JoySticks[Name] = Value;
    }
    //Удаление джойстика
    public static void RemoveJoystick(string Name)
    {
        JoySticks.Remove(Name);
    }
}
