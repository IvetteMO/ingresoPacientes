using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ingresoPacientes
{
    public partial class MainForm : Form
    {
        private List<Paciente> listaPacientes = new List<Paciente>();

        public MainForm()
        {
            InitializeComponent();
        }

        // Clase para almacenar la información del paciente
        private class Paciente
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int Edad { get; set; }
            public string Colonia { get; set; }
            public List<string> Sintomas { get; set; }
            public string Genero { get; set; }
            public string TipoIngreso { get; set; }
            public DateTime FechaIngreso { get; set; }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Inicializa el ComboBox de colonias con datos de ejemplo
            cbmColonia.Items.Add("Colonia A");
            cbmColonia.Items.Add("Colonia B");
            cbmColonia.Items.Add("Colonia C");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validar campos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text) ||
                cbmColonia.SelectedItem == null)
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear un nuevo paciente con los datos ingresados
            Paciente nuevoPaciente = new Paciente
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Edad = Convert.ToInt32(txtEdad.Text),
                Colonia = cbmColonia.SelectedItem.ToString(),
                Sintomas = ObtenerSintomasSeleccionados(),
                Genero = rbMasculino.Checked ? "Masculino" : "Femenino",
                TipoIngreso = rbUrgencia.Checked ? "Emergencia" : "Programado",
                FechaIngreso = dateTimePickerFecha.Value
            };

            // Agregar el paciente a la lista y limpiar los campos
            listaPacientes.Add(nuevoPaciente);
            LimpiarCampos();
            //MostrarPacientes();
            dgvPacientes.DataSource = null;
            dgvPacientes.DataSource = listaPacientes;
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
            cbmColonia.SelectedIndex = -1;
            LimpiarCheckBoxes();
            rbMasculino.Checked = false;
            rbUrgencia.Checked = false;
            dateTimePickerFecha.Value = DateTime.Now;
        }

        private List<string> ObtenerSintomasSeleccionados()
        {
            List<string> sintomas = new List<string>();

            if (chkFiebre.Checked)
                sintomas.Add("Fiebre");

            if (chkCabeza.Checked)
                sintomas.Add("Tos");

            if (chkDolorGarganta.Checked)
                sintomas.Add("Dolor de garganta");

            // Agrega más síntomas si es necesario

            return sintomas;
        }

        private void LimpiarCheckBoxes()
        {
            foreach (CheckBox chk in gbxSintomas.Controls)
            {
                chk.Checked = false;
            }
        }

        //private void MostrarPacientes()
        //{
        //    // Limpia el DataGridView antes de mostrar la lista de pacientes
        //    dgvPacientes.Rows.Clear();

        //    foreach (Paciente paciente in listaPacientes)
        //    {
        //        dgvPacientes.Rows.Add(
        //            paciente.Nombre,
        //            paciente.Apellido,
        //            paciente.Edad,
        //            paciente.Colonia,
        //            string.Join(", ", paciente.Sintomas),
        //            paciente.Genero,
        //            paciente.TipoIngreso,
        //            paciente.FechaIngreso.ToString("dd/MM/yyyy HH:mm")
        //        );
        //    }
        //}


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //listaPacientes.Clear();
            //dgvPacientes.Rows.Clear();
            LimpiarCampos();
        }
    }


}
