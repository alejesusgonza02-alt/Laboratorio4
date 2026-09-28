using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ejemplo_en_clase_1
{
    public partial class Form1 : Form
    {
        private string rutaImagen = "";
        public Form1()
        {
            InitializeComponent();

            Conexion.CrearBaseDatos();
            CargarProductos();

            dgvProductos.RowTemplate.Height = 80;
        }

        private void CargarProductos()
        {
            dgvProductos.Rows.Clear();

            using (var conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string sql = "SELECT Id, Producto, Precio, Cantidad, Imagen FROM Productos";

                using (var comando = new System.Data.SQLite.SQLiteCommand(sql, conexion))
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        int fila = dgvProductos.Rows.Add();

                        dgvProductos.Rows[fila].Cells["colId"].Value = lector["Id"];
                        dgvProductos.Rows[fila].Cells["colProducto"].Value = lector["Producto"];
                        dgvProductos.Rows[fila].Cells["colPrecio"].Value = lector["Precio"];
                        dgvProductos.Rows[fila].Cells["colCantidad"].Value = lector["Cantidad"];

                        string nombreImagen = lector["Imagen"].ToString();

                        if (nombreImagen != "")
                        {
                            string ruta = Path.Combine(Application.StartupPath, "Imagenes", nombreImagen);

                            if (File.Exists(ruta))
                            {
                                using (Image imagen = Image.FromFile(ruta))
                                {
                                    dgvProductos.Rows[fila].Cells["colImagen"].Value = new Bitmap(imagen);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void lblBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtPrecio.Text == "" || txtCantidad.Text == "")
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            decimal precio;
            int cantidad;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("El precio debe ser un número.");
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("La cantidad debe ser un número entero.");
                return;
            }

            string nombreImagen = "";

            if (rutaImagen != "")
            {
                string carpeta = Path.Combine(Application.StartupPath, "Imagenes");

                Directory.CreateDirectory(carpeta);

                nombreImagen = Guid.NewGuid().ToString() + Path.GetExtension(rutaImagen);

                string destino = Path.Combine(carpeta, nombreImagen);

                File.Copy(rutaImagen, destino, true);
            }

            using (var conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string sql = "INSERT INTO Productos (Producto, Precio, Cantidad, Imagen) VALUES (@Producto, @Precio, @Cantidad, @Imagen)";

                using (var comando = new System.Data.SQLite.SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Producto", txtNombre.Text);
                    comando.Parameters.AddWithValue("@Precio", Convert.ToDouble(precio));
                    comando.Parameters.AddWithValue("@Cantidad", cantidad);
                    comando.Parameters.AddWithValue("@Imagen", nombreImagen);

                    comando.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Producto guardado correctamente.");

            CargarProductos();

            txtNombre.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();

            rutaImagen = "";
            picProducto.Image = null;
        }

        private void picProducto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    rutaImagen = dialogo.FileName;

                    using (Image imagen = Image.FromFile(rutaImagen))
                    {
                        picProducto.Image = new Bitmap(imagen);
                    }
                }
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            txtNombre.Text = dgvProductos.Rows[e.RowIndex].Cells["colProducto"].Value.ToString();
            txtPrecio.Text = dgvProductos.Rows[e.RowIndex].Cells["colPrecio"].Value.ToString();
            txtCantidad.Text = dgvProductos.Rows[e.RowIndex].Cells["colCantidad"].Value.ToString();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            if (txtNombre.Text == "" || txtPrecio.Text == "" || txtCantidad.Text == "")
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            decimal precio;
            int cantidad;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("El precio debe ser un número.");
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("La cantidad debe ser un número entero.");
                return;
            }

            int id = Convert.ToInt32(
                dgvProductos.CurrentRow.Cells["colId"].Value
            );

            string nombreImagen = "";

            // Si se seleccionó una nueva imagen
            if (rutaImagen != "")
            {
                string carpeta = Path.Combine(Application.StartupPath, "Imagenes");

                Directory.CreateDirectory(carpeta);

                nombreImagen = Guid.NewGuid().ToString() + Path.GetExtension(rutaImagen);

                string destino = Path.Combine(carpeta, nombreImagen);

                File.Copy(rutaImagen, destino, true);
            }
            else
            {
                // Mantener la imagen que ya tenía el producto
                nombreImagen = dgvProductos.CurrentRow.Cells["colImagen"].Value != null
                    ? ""
                    : "";
            }

            using (var conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string sql;

                if (rutaImagen != "")
                {
                    sql = @"UPDATE Productos
                    SET Producto = @Producto,
                        Precio = @Precio,
                        Cantidad = @Cantidad,
                        Imagen = @Imagen
                    WHERE Id = @Id";
                }
                else
                {
                    sql = @"UPDATE Productos
                    SET Producto = @Producto,
                        Precio = @Precio,
                        Cantidad = @Cantidad
                    WHERE Id = @Id";
                }

                using (var comando = new System.Data.SQLite.SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Producto", txtNombre.Text);
                    comando.Parameters.AddWithValue("@Precio", Convert.ToDouble(precio));
                    comando.Parameters.AddWithValue("@Cantidad", cantidad);
                    comando.Parameters.AddWithValue("@Id", id);

                    if (rutaImagen != "")
                    {
                        comando.Parameters.AddWithValue("@Imagen", nombreImagen);
                    }

                    comando.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Producto modificado correctamente.");

            CargarProductos();

            txtNombre.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();

            picProducto.Image = null;
            rutaImagen = "";

            dgvProductos.ClearSelection();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            int id = Convert.ToInt32(
                dgvProductos.CurrentRow.Cells["colId"].Value
            );

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar este producto?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.No)
            {
                return;
            }

            using (var conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string sql = "DELETE FROM Productos WHERE Id = @Id";

                using (var comando = new System.Data.SQLite.SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);
                    comando.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Producto eliminado correctamente.");

            CargarProductos();

            txtNombre.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();

            picProducto.Image = null;
            rutaImagen = "";
        }

        private void picBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                MessageBox.Show("Escriba un producto para buscar.");
                return;
            }

            dgvProductos.Rows.Clear();

            bool encontrado = false;

            using (var conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();

                string sql = @"SELECT Id, Producto, Precio, Cantidad, Imagen
                       FROM Productos
                       WHERE Producto LIKE @Buscar";

                using (var comando = new System.Data.SQLite.SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Buscar", "%" + txtBuscar.Text + "%");

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            encontrado = true;

                            int fila = dgvProductos.Rows.Add();

                            dgvProductos.Rows[fila].Cells["colId"].Value = lector["Id"];
                            dgvProductos.Rows[fila].Cells["colProducto"].Value = lector["Producto"];
                            dgvProductos.Rows[fila].Cells["colPrecio"].Value = lector["Precio"];
                            dgvProductos.Rows[fila].Cells["colCantidad"].Value = lector["Cantidad"];

                            string nombreImagen = lector["Imagen"].ToString();

                            if (nombreImagen != "")
                            {
                                string ruta = Path.Combine(
                                    Application.StartupPath,
                                    "Imagenes",
                                    nombreImagen
                                );

                                if (File.Exists(ruta))
                                {
                                    using (Image imagen = Image.FromFile(ruta))
                                    {
                                        dgvProductos.Rows[fila].Cells["colImagen"].Value =
                                            new Bitmap(imagen);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!encontrado)
            {
                MessageBox.Show("Producto no registrado.");
                CargarProductos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            txtBuscar.Clear();

            picProducto.Image = null;
            rutaImagen = "";

            CargarProductos();

            dgvProductos.ClearSelection();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
