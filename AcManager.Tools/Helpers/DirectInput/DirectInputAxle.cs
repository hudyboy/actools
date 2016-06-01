using AcTools.Utils;
using AcTools.Utils.Helpers;

namespace AcManager.Tools.Helpers.DirectInput {
    public sealed class DirectInputAxle : BaseInputProvider<double>, IDirectInputProvider {
        public DirectInputAxle(DirectInputDevice device, int id) : base(id) {
            Device = device;
            ShortName = (id + 1).ToInvariantString();
            DisplayName = $"Axle {ShortName}";
        }

        public DirectInputDevice Device { get; }

        private double _roundedValue;

        public double RoundedValue {
            get { return _roundedValue; }
            set {
                if (Equals(value, _roundedValue)) return;
                _roundedValue = value;
                OnPropertyChanged();
            }
        }

        private double _delta;

        public double Delta {
            get { return _delta; }
            set {
                if (Equals(value, _delta)) return;
                _delta = value;
                OnPropertyChanged();
            }
        }

        protected override void OnValueChanged() {
            var value = Value;
            if ((value - RoundedValue).Abs() < 0.01) return;

            Delta = value - RoundedValue;
            RoundedValue = value;
        }
    }
}