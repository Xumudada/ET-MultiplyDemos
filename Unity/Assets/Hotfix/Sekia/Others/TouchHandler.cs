namespace ETHotfix
{
    public enum TouchEvent
    {
        Down,
        Up,
        Press
    }

    //已注册的按键
    public enum TOUCH_KEY
    {
        Idle = 0,
        Run = 1 << 0,
        Attack = 1 << 1,
        Skill1 = 1 << 2,
        Skill2 = 1 << 3,
        Skill3 = 1 << 4,
        Skill4 = 1 << 5,
        Summon1 = 1 << 6, //召唤师技能1
        Summon2 = 1 << 7, //召唤师技能2
        Summon3 = 1 << 8, //回城
    }

    public interface ITouchHandler
    {
        void Clear();

        bool IsTouched(TOUCH_KEY key);

        void Touch(TOUCH_KEY key);

        void Release(TOUCH_KEY key);
    }

    //按键操作接口
    public class TouchHandler : ITouchHandler
    {
        private static TouchHandler instance = null;
        private int mKeyValue; //保存当前键位
        public delegate void KeyChanged(TouchEvent e, TOUCH_KEY key); //注册委托类型
        public KeyChanged OnKeyChanged; //注册委托方法

        public static TouchHandler GetInstance()
        {
            if (instance == null)
            {
                instance = new TouchHandler();
                instance.mKeyValue = 0;
            }
            return instance;
        }

        private TouchHandler()
        {
        }

        public void Clear() //清理键位
        {
            mKeyValue = 0;
        }

        public bool IsTouched(TOUCH_KEY key) //是否已按下
        {
            return IsTouched(key, mKeyValue);
        }

        public bool IsTouched(TOUCH_KEY key, int compKey)
        {
            return (compKey & (int)key) != 0;
        }

        public void Touch(TOUCH_KEY key) //按下
        {
            Touch(key, ref mKeyValue);
        }

        public void Touch(TOUCH_KEY key, ref int compKey)
        {
            if (OnKeyChanged != null && !IsTouched(key))
                OnKeyChanged(TouchEvent.Down, key);
            compKey |= (int)key;
        }

        public void Release(TOUCH_KEY key) //松开
        {
            Release(key, ref mKeyValue);
        }

        public void Release(TOUCH_KEY key, ref int compKey)
        {
            if (OnKeyChanged != null && IsTouched(key))
                OnKeyChanged(TouchEvent.Up, key);
            compKey &= ~(int)key; //0&0为0  1&0为0
        }
    }
}
