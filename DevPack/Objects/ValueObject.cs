//namespace System
//{
//    public class Aaa : ValueObject<string>
//    {
//        protected Aaa(string value) : base(value)
//        {
//            MyProperty = "Test";

//            var a = new Aaa("teste");

//            if (a == value)
//            {
//            }
//        }

//        public ValueObject<string> MyProperty { get; set; }


//    }


//    public abstract class ValueObject<T>
//    {
//        public T Value { get; }

//        public static implicit operator T(ValueObject<T> valueOf) => valueOf.Value;

//        protected ValueObject(T value)
//        {
//            Value = value;
//        }

//        public static bool operator ==(String? a, String? b);

//        public override bool Equals(object obj)
//        {
//            if (obj is null)
//                return false;

//            if (obj is ValueObject<T> valueOf)
//                return Value.Equals(valueOf.Value);

//            return Value.Equals(obj);
//        }

//        public override int GetHashCode()
//        {
//            if (Value is null)
//                return 0;

//            return Value.GetHashCode();
//        }
//    }
//}
