using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libreta_Contactos
{
    public partial class LibretaContactos : Form
    {
        public LibretaContactos()
        {
            InitializeComponent();
        }

        private void LibretaContactos_Load(object sender, EventArgs e)
        {
            ListarContactos();
        }

        //metodo2 con el datagrid asignado para reutilizar 
        public void ListarContactos() {

            Contactos contacto = new Contactos();
            contacto.CargarContacos(dtgContacto);

        }

        private void bpnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string correo = txtCorreo.Text;
            string fono = txtFono.Text;
            string tipo = cmbTipo.Text;

            if (nombre == "" || correo == "" || fono == "" || tipo == "Seleccione tipo")
            { 
                MessageBox.Show("Debe completar todos los campos");
            }
            else {
               Contactos nuevoContacto = new Contactos(0, nombre, correo, fono, tipo);
                int fila = nuevoContacto.AgregarContacto();
                if (fila == 1)
                {
                    MessageBox.Show("El registro se agrego correctamente", "Existo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetearFormulario();
                    ListarContactos();
                }
                else {
                    MessageBox.Show("Ocurrio un problema al agregar el registro", "Existo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Evento Cellclick se activa cuando el usuario toca una fila
        private void dtgContacto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //representa el índice de la fila donde se hizo clic.
            int indice = e.RowIndex;

            //-1 es la fila con los nombres de los campos y si en la 2 celda esta vacio no deberia dar informacion
            if (indice == -1 || dtgContacto.SelectedCells[1].Value.ToString() == "")
            {
                ResetearFormulario();
            }
            else {
                txtId.Text = dtgContacto.SelectedCells[0].Value.ToString();
                txtNombre.Text = dtgContacto.SelectedCells[1].Value.ToString();
                txtCorreo.Text = dtgContacto.SelectedCells[2].Value.ToString();
                txtFono.Text = dtgContacto.SelectedCells[3].Value.ToString();
                cmbTipo.Text = dtgContacto.SelectedCells[4].Value.ToString();

                btnAgregar.Enabled = false;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
            }
        }

        public void ResetearFormulario() {

            txtId.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtFono.Clear();
            cmbTipo.Text = "Seleccione tipo";

            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            //Focus te manda a escribir directamente en nombre luego de limpiar los campos
            txtNombre.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearFormulario();  
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);

            DialogResult confirmar = MessageBox.Show("¿Desea eliminar?", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (confirmar == DialogResult.OK)
            {
                Contactos contacto = new Contactos(id);
                int fila = contacto.EliminarContacto();

                if (fila == 1)
                {
                    MessageBox.Show("Elimino el contacto exitosamente");
                    ResetearFormulario();
                    ListarContactos();
                }
                else {
                    MessageBox.Show("No se pudo eliminar el contacto");
                }
            }
            else {
                ResetearFormulario();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            string nombre = txtNombre.Text;
            string correo = txtCorreo.Text;
            string fono = txtFono.Text; 
            string tipo = cmbTipo.Text;

            DialogResult confirmar = MessageBox.Show("Desea realizar los cambios?", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (confirmar == DialogResult.OK)
            {
               Contactos contacto = new Contactos(id,nombre, correo, fono,tipo);
                int fila = contacto.EditarContacto();

                if (fila == 1)
                {
                    MessageBox.Show("Actualizo el registro correctamente");
                    ResetearFormulario();
                    ListarContactos();
                }
                else {
                    MessageBox.Show("No se pudo actualizar el contacto");
                }
            }
            else {
                ResetearFormulario();
            }

        }

    }
}
