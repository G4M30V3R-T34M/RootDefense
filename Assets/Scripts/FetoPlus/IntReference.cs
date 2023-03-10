using System;

[Serializable]
public class IntReference
{
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public IntReference() { }

        public IntReference(int value) {
            UseConstant = false;
            ConstantValue = value;
        }

        public int Value {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator int(IntReference reference) {
            return reference.Value;
        }

        public override string ToString() => Value.ToString();
}
