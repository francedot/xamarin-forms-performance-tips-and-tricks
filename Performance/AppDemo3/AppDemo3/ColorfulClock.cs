using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xamarin.Forms;

namespace AppDemo3
{
    public class ColorfulClock : ContentView
    {
        public class ColorPaletteItem
        {
            [JsonProperty(PropertyName = "hex")]
            public string Hex { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "rgb")]
            public string Rgb { get; set; }
        }

        public struct HandParams
        {
            public HandParams(double width, double height, double offset) : this()
            {
                Width = width;
                Height = height;
                Offset = offset;
            }

            public double Width { get; }   // fraction of radius
            public double Height { get; }  // ditto
            public double Offset { get; }  // relative to center pivot
        }

        private readonly HandParams _secondParams = new HandParams(0.02, 1.1, 0.85);
        private readonly HandParams _minuteParams = new HandParams(0.05, 0.8, 0.9);
        private readonly HandParams _hourParams = new HandParams(0.125, 0.65, 0.9);

        private readonly BoxView[] _tickMarks = new BoxView[60];
        private readonly BoxView _secondHand;
        private readonly BoxView _minuteHand;
        private readonly BoxView _hourHand;

        private const string ColorsFileName = "colors.json";

        public IEnumerable<Color> LoadColors()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream($"AppDemo3.{ColorsFileName}"))
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<List<ColorPaletteItem>>(reader.ReadToEnd())
                    .Select(c => Color.FromHex(c.Hex));
            }
        }

        public ColorfulClock()
        {
            var absoluteLayout = new AbsoluteLayout();

            var colors = LoadColors().ToList();

            // Create the tick marks (to be sized and positioned later)
            for (var i = 0; i < _tickMarks.Length; i++)
            {
                _tickMarks[i] = new BoxView
                {
                    Color = colors.ElementAtOrDefault(i)
                };
                absoluteLayout.Children.Add(_tickMarks[i]);
            }

            // Create the three hands.
            absoluteLayout.Children.Add(_hourHand = new BoxView
            {
                Color = colors.ElementAt(new Random().Next(colors.Count))
            });
            absoluteLayout.Children.Add(_minuteHand = new BoxView
            {
                Color = colors.ElementAt(new Random().Next(colors.Count))
            });
            absoluteLayout.Children.Add(_secondHand = new BoxView
            {
                Color = colors.ElementAt(new Random().Next(colors.Count))
            });

            // Size and position the 12 tick marks.
            const double w = 412.0;
            const double h = 660.0;
            var center = new Point(w / 2, h / 2);
            var radius = 0.45 * Math.Min(w, h);

            for (var i = 0; i < _tickMarks.Length; i++)
            {
                var size = radius / (i % 5 == 0 ? 15 : 30);
                var radians = i * 2 * Math.PI / _tickMarks.Length;
                var x = center.X + radius * Math.Sin(radians) - size / 2;
                var y = center.Y - radius * Math.Cos(radians) - size / 2;
                AbsoluteLayout.SetLayoutBounds(_tickMarks[i], new Rectangle(x, y, size, size));

                _tickMarks[i].AnchorX = 0.51;        // Anchor settings necessary for Android
                _tickMarks[i].AnchorY = 0.51;
                _tickMarks[i].Rotation = 180 * radians / Math.PI;
            }

            // Function for positioning and sizing hands.
            Action<BoxView, HandParams> layout = (boxView, handParams) =>
            {
                var width = handParams.Width * radius;
                var height = handParams.Height * radius;
                var offset = handParams.Offset;

                AbsoluteLayout.SetLayoutBounds(boxView,
                    new Rectangle(center.X - 0.5 * width,
                                    center.Y - offset * height,
                                        width, height));

                boxView.AnchorX = 0.51;
                boxView.AnchorY = handParams.Offset;
            };

            layout(_secondHand, _secondParams);
            layout(_minuteHand, _minuteParams);
            layout(_hourHand, _hourParams);

            this.Content = absoluteLayout;

            Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimerTick);
        }

        private bool OnTimerTick()
        {
            // Set rotation angles for hour and minute hands.
            var dateTime = DateTime.Now;
            _hourHand.Rotation = 30 * (dateTime.Hour % 12) + 0.5 * dateTime.Minute;
            _minuteHand.Rotation = 6 * dateTime.Minute + 0.1 * dateTime.Second;

            // Do an animation for the second hand.
            var t = dateTime.Millisecond / 1000.0;
            if (t < 0.5)
            {
                t = 0.5 * Easing.SpringIn.Ease(t / 0.5);
            }
            else
            {
                t = 0.5 * (1 + Easing.SpringOut.Ease((t - 0.5) / 0.5));
            }
            _secondHand.Rotation = 6 * (dateTime.Second + t);

            return true;
        }
    }
}
