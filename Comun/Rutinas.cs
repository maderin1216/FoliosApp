using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace FoliosApp.Comun
{
    public static class Rutinas
    {
        public static string GetNombreMetodo([CallerMemberName] string nombreMetodo = null)
        {
            return nombreMetodo;
        }

        public static string GetMensajeError(string mensaje, [CallerMemberName] string nombreMetodo = null)
        {
            var stackTrace = new StackTrace();
            var frame = stackTrace.GetFrame(1); // 1 = método que llamó a este
            var metodo = frame.GetMethod();
            var nombreCompleto = $"{metodo.DeclaringType?.Namespace}.{metodo.DeclaringType?.Name}.{nombreMetodo}";

            return $"[{nombreCompleto}]: {mensaje}";
        }

        public static void FormatoGrilla(DataGridView aDvg)
        {
            aDvg.AllowUserToAddRows = false;
            aDvg.AllowUserToDeleteRows = false;
            aDvg.AllowUserToResizeRows = false;
            aDvg.EditMode = DataGridViewEditMode.EditOnEnter;
            aDvg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            aDvg.MultiSelect = false;
            aDvg.RowHeadersVisible = false;
            aDvg.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            aDvg.BorderStyle = BorderStyle.None;
            aDvg.RowTemplate.Height = 35;
            aDvg.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 156, 221);
            aDvg.DefaultCellStyle.SelectionForeColor = Color.White;
            aDvg.DefaultCellStyle.Font = new Font("Calibri", 12, FontStyle.Regular);
            aDvg.BackgroundColor = Color.FromName("Control");
            aDvg.ReadOnly = true;

            aDvg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            aDvg.EnableHeadersVisualStyles = false;
            aDvg.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            aDvg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            aDvg.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 13, FontStyle.Bold);
            aDvg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(204, 204, 204);
            aDvg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            aDvg.ColumnHeadersHeight = 50;

            foreach (DataGridViewColumn c in aDvg.Columns)
            {
                if (c.AutoSizeMode != DataGridViewAutoSizeColumnMode.None)
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        public static string GenerarMD5(string usuario, string pass, Error error)
        {
            StringBuilder sb = new StringBuilder();
            string sSemilla = "AvAvellaneda498";

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes($"{usuario}{pass}{sSemilla}");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }
    }
}
