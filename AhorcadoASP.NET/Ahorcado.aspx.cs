using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AhorcadoASP.NET
{
    public partial class Ahorcado : System.Web.UI.Page
    {
        private static readonly string[] palabras = { "MARIPOSA", "LABERINTO", "HORIZONTE", "CARAMELO", "NIEBLA" };
        private string palabraSeleccionada;
        private char[] palabraOculta;
        private int intentosRestantes;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartGame();
            }
            else
            {
                palabraSeleccionada = (string)Session["palabraSeleccionada"];
                palabraOculta = (char[])Session["palabraOculta"];
                intentosRestantes = (int)Session["intentosRestantes"];
                lblPalabra.Text = new string(palabraOculta);
                LabelIntentosRestantes.Text = $"Intentos restantes: {intentosRestantes}";
                Image.ImageUrl = (string)Session["imagen"];
            }

            AddLetters();
        }

        private void StartGame()
        {
            Random rand = new Random();
            palabraSeleccionada = palabras[rand.Next(palabras.Length)];
            palabraOculta = new string('_', palabraSeleccionada.Length * 2 - 1).ToCharArray(); // Esto se hace para dejar espacio entre cada letra en la palabra oculta.
            for (int i = 1; i < palabraOculta.Length; i += 2)
            {
                palabraOculta[i] = ' ';
            }
            intentosRestantes = 7;

            lblPalabra.Text = new string(palabraOculta);
            LabelIntentosRestantes.Text = $"Intentos restantes: {intentosRestantes}";

            // Guardar el estado en la sesión
            Session["palabraSeleccionada"] = palabraSeleccionada;
            Session["palabraOculta"] = palabraOculta;
            Session["intentosRestantes"] = intentosRestantes;
        }

        public void AddLetters()
        {
            string[] teclas = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            foreach (string tecla in teclas)
            {
                Button btn = new Button();
                btn.Text = tecla;
                btn.CssClass = "tecla";
                btn.Click += new EventHandler(Tecla_Click);
                PanelLetras.Controls.Add(btn);
            }
        }

        protected void Tecla_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string letra = btn.Text;
            btn.Enabled = false;

            bool acierto = false;
            for (int i = 0; i < palabraSeleccionada.Length; i++)
            {
                if (palabraSeleccionada[i] == letra[0])
                {
                        // Coloca la letra en los índices pares
                        palabraOculta[i * 2] = letra[0];
                        acierto = true; 
                }

            }

            if (!acierto)
            {
                LabelLetrasUtilizadas.Text += btn.Text + " ";
                intentosRestantes--;
                UpdateImage(intentosRestantes);
            }
 
            lblPalabra.Text = new string(palabraOculta);
            LabelIntentosRestantes.Text = $"Intentos restantes: {intentosRestantes}";

            // Actualizar el estado en la sesión
            Session["palabraOculta"] = palabraOculta;
            Session["intentosRestantes"] = intentosRestantes;
            Session["imagen"] = Image.ImageUrl;

            if (intentosRestantes == 0)
            {
                LabelIntentosRestantes.Text = "Has perdido. La palabra era: " + palabraSeleccionada;
                DisabledTeclas();
            }
            else if (!palabraOculta.Contains('_'))
            {
                LabelIntentosRestantes.Text = "¡Felicidades! Has ganado.";
                DisabledTeclas();
            }
        }

        private void DisabledTeclas()
        {
            foreach (Control ctrl in PanelLetras.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Enabled = false;
                }
            }
        }

        private void UpdateImage(int intentosRestantes)
        {
            string[] imagenes = {
            "",
            "~/img/Ahorcado0.jpg",
            "~/img/Ahorcado1.jpg",
            "~/img/Ahorcado2.jpg",
            "~/img/Ahorcado3.jpg",
            "~/img/Ahorcado4.jpg",
            "~/img/Ahorcado5.jpg",
            "~/img/Ahorcado6.jpg"
         };

            Image.ImageUrl = imagenes[7 - intentosRestantes];
        }
    }
}