using System.Collections.Generic;
using System.Linq;

namespace System
{
    public struct Cpf
    {
        public Cpf(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));

            if (!IsValid(value))
                throw new FormatException($"The '{value}' is a invalid CPF.'");

            Value = RemoveFormat(value);
        }

        private string Value { get; }

        public static implicit operator Cpf(string value) => new(value);

        public static bool operator !=(Cpf left, Cpf right)
        {
            return !(left == right);
        }

        public static bool operator !=(string left, Cpf right)
        {
            return !(left == right);
        }

        public static bool operator !=(Cpf left, string right)
        {
            return !(left == right);
        }

        public static bool operator ==(Cpf left, string right)
        {
            if (left.Value is null && right is null)
                return true;

            if (left.Value is null && right is not null)
                return false;

            return left.Value.Equals(RemoveFormat(right));
        }

        public static bool operator ==(string left, Cpf right)
        {
            return right.Value.Equals(RemoveFormat(left));
        }

        public static bool operator ==(Cpf left, Cpf right)
        {
            return left.Value.Equals(right.Value);
        }

        public static bool TryParse(string cpfValue, out Cpf cpf)
        {
            if (IsValid(cpfValue))
            {
                cpf = new Cpf(cpfValue);

                return true;
            }
            else
            {
                cpf = default;

                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is Cpf cpf)
                return Value.Equals(cpf.Value);

            return Value.Equals(RemoveFormat(obj.ToString()));
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString() => Value;

        public string ToStringFormated() => string.Format(@"{0:###\.###\.###\-##}", long.Parse(Value));

        private static bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;
            var dv1 = 0;
            var dv2 = 0;

            bool digitosIdenticos = true;
            var ultimoDigito = -1;

            foreach (var c in value)
            {
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (posicao != 0 && ultimoDigito != digito)
                        digitosIdenticos = false;

                    ultimoDigito = digito;

                    if (posicao < 9)
                    {
                        totalDigito1 += digito * (10 - posicao);
                        totalDigito2 += digito * (11 - posicao);
                    }
                    else if (posicao == 9)
                        dv1 = digito;
                    else if (posicao == 10)
                        dv2 = digito;

                    posicao++;
                }
            }

            if (posicao > 11)
                return false;

            if (digitosIdenticos)
                return false;

            var digito1 = totalDigito1 % 11;

            digito1 = digito1 < 2
                ? 0
                : 11 - digito1;

            if (dv1 != digito1)
                return false;

            totalDigito2 += digito1 * 2;

            var digito2 = totalDigito2 % 11;

            digito2 = digito2 < 2 ? 0 : 11 - digito2;

            return dv2 == digito2;
        }

        private static string RemoveFormat(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            return new string(_removeFormat(value).ToArray());

            IEnumerable<char> _removeFormat(string value)
            {
                foreach (var c in value)
                {
                    if (char.IsDigit(c))
                        yield return c;
                }
            }
        }
    }
}
