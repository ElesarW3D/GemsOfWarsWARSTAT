using System.Windows;

namespace GemsOfWarsWARSTAT.Services
{
    public enum DataGridItemVisibility
    {
        StandartDataGrid,
        LevelDataGrid,
        TableDataGrid
    }

    public class OneDataGridVisibility : VisibilityOneEnumItem<DataGridItemVisibility>
    {
        public Visibility StandartDataGridVisibility
        {
            get { return _controls[DataGridItemVisibility.StandartDataGrid]; }
        }

        public Visibility LevelDataGridVisibility
        {
            get { return _controls[DataGridItemVisibility.LevelDataGrid]; }
        }
        public Visibility TableDataGridVisibility
        {
            get { return _controls[DataGridItemVisibility.TableDataGrid]; }
        }
    }
}
