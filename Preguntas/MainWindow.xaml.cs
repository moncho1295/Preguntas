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
        protected string _textoPregunta;

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

        internal virtual UIElement RenderControl()
        {
            TextBlock tb = new TextBlock();
            tb.Text = "Error de sintaxis en la línea: " + _linea;
            return tb;
        }

        protected UIElement AdornarStackPanel(StackPanel sp)
        {
            sp.Margin = new Thickness(5);
            return sp;
        }
    }

    // Clase encargada de las preguntas de tipo única
    public class TipoUnica : Pregunta
    {
        private string _textoRespuesta;
        private TextBlock _tbRespuesta;
        private bool _respuestaEsVisible = false;
        public TipoUnica(string linea) : base(linea)
        {
            _textoRespuesta = _partes[2];
        }

        // Sobrecarga de métodos (Hijo -> Padre)
        internal override UIElement RenderControl()
        {
            StackPanel sp = new StackPanel();
            TextBlock tbPregunta = new TextBlock();
            tbPregunta.Text = _textoPregunta;
            tbPregunta.Cursor = Cursors.Hand;
            _tbRespuesta = new TextBlock();
            _tbRespuesta.Text = _textoRespuesta;

            // Agregando un nuevo manejador de eventos al clic de la pregunta
            tbPregunta.MouseDown += new MouseButtonEventHandler(tbPregunta_MouseDown);

            // Ocultando la respuesta
            _tbRespuesta.Visibility = Visibility.Collapsed;

            sp.Children.Add(tbPregunta);
            sp.Children.Add(_tbRespuesta);

            return AdornarStackPanel(sp);
        }

        void tbPregunta_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_respuestaEsVisible)
                _tbRespuesta.Visibility = Visibility.Collapsed;
            else
                _tbRespuesta.Visibility = Visibility.Visible;

            _respuestaEsVisible = !_respuestaEsVisible;
        }
    }

    // Clase encargada de las preguntas de tipo múltiple
    public class RespuestasMultiples : Pregunta
    {
        private string _textoRespuesta1;
        private string _textoRespuesta2;
        private string _textoRespuesta3;
        private string _textoRespuesta4;
        public RespuestasMultiples(string linea) : base(linea)
        {
            _textoRespuesta1 = _partes[2];
            _textoRespuesta2 = _partes[3];
            _textoRespuesta3 = _partes[4];
            _textoRespuesta4 = _partes[5];
        }

        internal override UIElement RenderControl()
        {
            StackPanel sp = new StackPanel();
            TextBlock tbPregunta = new TextBlock();
            tbPregunta.Text = _textoPregunta;

            TextBlock _tbRespuesta1 = new TextBlock();
            _tbRespuesta1.Text = _textoRespuesta1;

            TextBlock _tbRespuesta2 = new TextBlock();
            _tbRespuesta2.Text = _textoRespuesta2;

            TextBlock _tbRespuesta3 = new TextBlock();
            _tbRespuesta3.Text = _textoRespuesta3;

            TextBlock _tbRespuesta4 = new TextBlock();
            _tbRespuesta4.Text = _textoRespuesta4;

            sp.Children.Add(tbPregunta);
            sp.Children.Add(_tbRespuesta1);
            sp.Children.Add(_tbRespuesta2);
            sp.Children.Add(_tbRespuesta3);
            sp.Children.Add(_tbRespuesta4);

            return AdornarStackPanel(sp);
        }
    }

    // Clase encargada de las preguntas de tipo verdadero / falso
    public class VerdaderoFalso : TipoUnica
    {
        public VerdaderoFalso(string linea) : base(linea)
        {
            _textoPregunta = "Verdadero o Falso: " + _textoPregunta;
        }
    }
}
