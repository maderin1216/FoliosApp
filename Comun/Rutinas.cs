using System.Drawing;
using System.Runtime.CompilerServices;
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
            return $"{nombreMetodo}: {mensaje}";
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
    }
}
