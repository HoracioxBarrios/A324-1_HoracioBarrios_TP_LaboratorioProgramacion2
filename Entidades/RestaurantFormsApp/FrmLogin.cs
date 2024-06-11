using Negocio;
namespace RestaurantFormsApp
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            RestoranInicio restoranInicio = new RestoranInicio();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(txtUser.Text) && string.IsNullOrWhiteSpace(txtPassword.Text)))
            { 
                //restoran
                
            
            }
            else
            {
                MessageBox.Show("Completar los campos por favor");
            }
        }
    }
}
