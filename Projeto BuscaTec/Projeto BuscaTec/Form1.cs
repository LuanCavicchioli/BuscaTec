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
    public partial class Form1 : Form
    {
        public string conexaoString;
        private SqlConnection conexaoDB;
        public Form1()
        {
            InitializeComponent();

            //String de conexão

            conexaoString = "Data Source=DESKTOP-4D8LF92;Initial Catalog=BuscaTec;Integrated Security=True";

            //Inicializando a conexão com o Banco de dados
            conexaoDB = new SqlConnection(conexaoString);
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtEmail.Text == "" ||  mskCpf.Text == "" || txtSenha.Text == "")
            {
                MessageBox.Show("PREENCHA TODAS AS COLUNAS", "AVISO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                // Verificar se o usuário já existe no banco de dados, pra isso usa um metodo
                if (UsuarioJaExiste(mskCpf.Text))
                {
                    MessageBox.Show("Este usuário já existe!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome.Text = "";
                    txtEmail.Text = "";
                    mskCpf.Text = "";
                    txtSenha.Text = "";
                    mskCelular.Text = "";
                }
                else
                {
                    // Se o usuário não existe, então podemos adicionar
                    string sql = "INSERT INTO Usuarios (nome,email,cpf,senha,celular)" +
                            "VALUES (@nome,@email,@cpf,@senha,@celular)";
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand(sql, conexaoDB);
                        sqlCmd.Parameters.AddWithValue("nome", txtNome.Text);
                        sqlCmd.Parameters.AddWithValue("email", txtEmail.Text);
                        sqlCmd.Parameters.AddWithValue("cpf", mskCpf.Text);
                        sqlCmd.Parameters.AddWithValue("senha", txtSenha.Text);
                        sqlCmd.Parameters.AddWithValue("celular", mskCelular.Text);

                        conexaoDB.Open();
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Cadastro Realizado com Sucesso!!!");
                        txtNome.Text = "";
                        txtEmail.Text = "";
                        mskCpf.Text = "";
                        txtSenha.Text = "";
                        mskCelular.Text = "";
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Erro ao carregar os dados: " + ex);
                    }
                    finally
                    {
                        conexaoDB.Close();
                    }
                }
            }
        }
        private bool UsuarioJaExiste(string cpf)
        {
            string sql = "SELECT COUNT(*) FROM Usuarios WHERE cpf = @cpf";
            SqlCommand sqlCmd = new SqlCommand(sql, conexaoDB);
            sqlCmd.Parameters.AddWithValue("cpf", cpf);

            conexaoDB.Open();
            int count = (int)sqlCmd.ExecuteScalar();
            conexaoDB.Close();

            return count > 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtSenha.PasswordChar = '\0';
            }
            else
            {
                txtSenha.PasswordChar = '*';
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login loginform = new Login();
            this.Hide();
            loginform.ShowDialog();
            this.Close();
            
        }
    }
}
