using Negocio;
namespace RestaurantFormsApp
{
    public partial class FrmLogin : Form
    {
        private Restoran _restoran;
        public FrmLogin()
        {
            InitializeComponent();
            _restoran = new Restoran();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(txtUser.Text) && string.IsNullOrWhiteSpace(txtPassword.Text)))
            { 
                
                //_restoran.GestorEmpleados.
            
            }
            else
            {
                MessageBox.Show("Completar los campos por favor");
            }
        }
    }
}
