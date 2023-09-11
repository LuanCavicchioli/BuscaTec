using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_BuscaTec
{

    public partial class Login : Form
    {
        public string conexaoString;
        private SqlConnection conexaoDB;
        public Login()
        {
            InitializeComponent();

            //String de conexão

            conexaoString = "Data Source=DESKTOP-4D8LF92;Initial Catalog=BuscaTec;Integrated Security=True";

            //Inicializando a conexão com o Banco de dados
            conexaoDB = new SqlConnection(conexaoString);
        }


     

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

            if (AutenticarUsuario(email, senha))
            {
  
                MessageBox.Show("Login bem-sucedido!");
 
            }
            else
            {

                MessageBox.Show("Login falhou. Verifique suas credenciais.");
            }
        }
        //Metodo utilizado para puxar os dados do cadastro, e assim ser utilizados para logar
        private bool AutenticarUsuario(string email, string senha)
        {
            string conexaoString = "Data Source=DESKTOP-4D8LF92;Initial Catalog=BuscaTec;Integrated Security=True";
            using (SqlConnection conexaoDB = new SqlConnection(conexaoString))
            {
                string sql = "SELECT COUNT(*) FROM Usuarios WHERE email = @email AND senha = @senha";
                SqlCommand cmd = new SqlCommand(sql, conexaoDB);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@senha", senha);

                conexaoDB.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }
    }
}
