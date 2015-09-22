using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Preguntas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Un listado de preguntas
            List<string> lineas = new List<string>();
            lineas.Add("tipoUnica;¿De que color es el cielo?;azul");
            lineas.Add("tipoUnica;¿De que color es el sol?;amarillo");
            lineas.Add("respuestaMultiple;¿Cuantos planetas hay en la via lactea;1;5;8;10");
            lineas.Add("verdaderoFalso;¿El sol se pone en el oeste?;verdadero");
            lineas.Add("respuestaMultiple;¿Cuantas lunas tiene la tierra?;1;2;5;7");

            // Imprimir las preguntas en la pantalla
            foreach (var linea in lineas)
	        {
                Pregunta pregunta = Pregunta.Crear(linea);
                MainStackPanel.Children.Add(pregunta.RenderControl());
	        }
        }
    }

    // Clase base para las preguntas
    public class Pregunta
    {
        private string _linea;
        protected string[] _partes;
        private string _textoPregunta;

        // Constructor
        public Pregunta(string linea)
        {
            _linea = linea;

            _partes = linea.Split(';');
            _textoPregunta = _partes[1];
        }

        public static Pregunta Crear(string linea)
        {
            if (linea.StartsWith("tipoUnica;"))
                return new TipoUnica(linea);
            else if (linea.StartsWith("respuestaMultiple;"))
                return new RespuestasMultiples(linea);
            else if (linea.StartsWith("verdaderoFalso;"))
                return new VerdaderoFalso(linea);
            else
                return new Pregunta(linea);
        }

        internal UIElement RenderControl()
        {
            TextBlock tb = new TextBlock();
            tb.Text = _linea;
            return tb;
        }
    }

    // Clase encargada de las preguntas de tipo única
    public class TipoUnica : Pregunta
    {
        private string _textoRespuesta;
        private TextBlock _tbRespuesta;
        public TipoUnica(string linea) : base(linea)
        {
            _textoRespuesta = _partes[2];
        }
    }

    // Clase encargada de las preguntas de tipo múltiple
    public class RespuestasMultiples : Pregunta
    {
        public RespuestasMultiples(string linea) : base(linea)
        {

        }
    }

    // Clase encargada de las preguntas de tipo verdadero / falso
    public class VerdaderoFalso : Pregunta
    {
        public VerdaderoFalso(string linea) : base(linea)
        {

        }
    }
}
