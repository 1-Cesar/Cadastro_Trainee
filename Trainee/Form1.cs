using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trainee
{
    public partial class Form1 : Form
    {
        //Configuração de Lista com base nos conceitos de POO
        List<Pessoa> lista;        
        BindingList<Pessoa> bdnLst;
        BindingSource bdnSrc;
        public Form1()
        {
            InitializeComponent();
            //Controles desabilitados
            btnEnviar.Enabled = false;
            btnExcluir.Enabled = false;          
            txtVaga.Enabled = false;
            txtNome.Enabled = false;
            txtSobrenome.Enabled = false;
            mskCpf.Enabled = false;
            dtpNascimento.Enabled = false;
            btnAdicionar.Enabled = false;
            btnLimpar.Enabled = false;
            //pega a data atual
            dtpNascimento.Text = DateTime.Now.ToString();
            
            lista = new List<Pessoa>();
            //Trabalhando com Grid
            bdnLst = new BindingList<Pessoa>(lista);
            bdnSrc = new BindingSource(bdnLst, null);
            grdLista.DataSource = bdnSrc;
        } 
        //Botão para adicionar candidados ao DataGrid
        private void btnAdicionar_Click(object sender, EventArgs e)
        {            
            btnEnviar.Enabled=true;
            btnExcluir.Enabled=true;
            txtVaga.Enabled=false;

            //definição de variáveis
            String maior, idadeString;
            int idade;            
            Boolean ok = true;

            //Percorre todos os controles para verificar inconsistencias
            foreach (Control controle in this.Controls)
            {
                if (controle is TextBox)
                {
                    if (controle.Text == String.Empty)
                    {
                        ok = false;
                        controle.BackColor = Color.Red;
                    }
                }
                else if (controle is MaskedTextBox)
                {
                    if (controle.Text.Length < 14)
                    {
                        ok = false;
                        mskCpf.BackColor = Color.Red;
                    }
                }
            }

            //Com tudo ok o programa continua
            if (ok)
            {                
                    //Verifica a data de nascimento e se o candidado é maior de idade
                    if (dtpNascimento.Value < DateTime.Now)
                    {
                        DateTime dataNascimento = dtpNascimento.Value;
                        idade = DateTime.Now.Year - dataNascimento.Year;
                        idadeString = idade.ToString();                        
                    if (idade >= 18)
                        {
                            maior = "Sim";
                        }
                        else
                        {
                            maior = "Não";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data inválida!");
                        return;
                    }

                //Verifica se já existe CPF cadastrado para vaga
                foreach (DataGridViewRow row in grdLista.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells["Cpf"].Value.Equals(mskCpf.Text))
                        {
                            MessageBox.Show("Voce esta tentando inserir o mesmo CPF");
                            return;

                        }

                    }
                }

                //Limitador de candidatos no Grid
                if (lista.Count == 3)
                {
                    MessageBox.Show("É permitido apenas 3 candidatos por vaga","Erro", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                //Populando o DataGridView - pegando apenas a data de nascimento do DateTimePicker
                lista.Add(new Pessoa(txtNome.Text, txtSobrenome.Text, mskCpf.Text, dtpNascimento.Value.ToShortDateString(), idadeString, maior, txtVaga.Text));
                bdnLst.ResetBindings();

                //Limpando dados já inseridos no DataGridView                
                txtNome.Clear();
                txtSobrenome.Clear();
                mskCpf.Clear();                
            }
            
            //Se existir dados inválidos a grid não é preenchida e é exibido uma mensagem de erro
            else
            {
                if (txtVaga.Text == String.Empty)
                {
                    MessageBox.Show("Nome da Vaga é obrigatório. O Cliente não foi adicionado à lista.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtVaga.Enabled = true;
                }
                else if (txtNome.Text == String.Empty)
                {
                    MessageBox.Show("Nome é obrigatório. O Cliente não foi adicionado à lista.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtSobrenome.Text == String.Empty)
                {
                    MessageBox.Show("Sobrenome é obrigatório. O Cliente não foi adicionado à lista.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mskCpf.Text.Length < 14)
                {
                    MessageBox.Show("CPF completo é obrigatório. O Cliente não foi adicionado à lista.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Campos Inválidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
        }        
        //botão para limpeza de campos
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtVaga.Clear();
            txtNome.Clear();
            txtSobrenome.Clear();
            mskCpf.Clear();
        }
        //Quando os campos recebem foco, voltam ao normal
        private void txtVaga_Enter(object sender, EventArgs e)
        {
            txtVaga.BackColor = Color.White;
            txtDisponivel.Enabled = false;
        }
        private void txtNome_Enter(object sender, EventArgs e)
        {
            txtNome.BackColor = Color.White;
            txtDisponivel.Enabled = false;
        }
        private void txtSobrenome_Enter(object sender, EventArgs e)
        {
            txtSobrenome.BackColor = Color.White;
            txtDisponivel.Enabled = false;
        }
        private void mskCpf_Enter(object sender, EventArgs e)
        {
            mskCpf.BackColor = Color.White;
            txtDisponivel.Enabled = false;
        }
        //Controles habilitados após informar a quantidade de vagas
        private void txtDisponivel_Enter(object sender, EventArgs e)
        {
            txtVaga.Enabled = true;
            txtNome.Enabled = true;
            txtSobrenome.Enabled = true;
            mskCpf.Enabled = true;
            dtpNascimento.Enabled = true;
            btnAdicionar.Enabled = true;
            btnLimpar.Enabled = true;            
        }
        //Envia os candidatos do DataGridView
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            txtVaga.Enabled = true;

            txtVaga.Clear();
            txtNome.Clear();
            txtSobrenome.Clear();
            mskCpf.Clear();
            btnExcluir.Enabled = false;
            btnEnviar.Enabled = false;
            
            //grdLista.DataSource = null;
            grdLista.Rows.Clear();
            MessageBox.Show("Candidatos cadastrados com Sucesso","Parabéns!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //Conversão
            int disponivel = int.Parse(txtDisponivel.Text);
            //Subtrai a quantidade de vagas restantes
            txtDisponivel.Text = (disponivel - 1).ToString();
            //Quando todas as vagas foram preenchidas o sistema retorna ao inicio
            if (txtDisponivel.Text == "0")
            {
                MessageBox.Show("Todas as vagas foram preenchidas","Muito bem!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                btnEnviar.Enabled = false;
                btnExcluir.Enabled = false;
                txtVaga.Enabled = false;
                txtNome.Enabled = false;
                txtSobrenome.Enabled = false;
                mskCpf.Enabled = false;
                dtpNascimento.Enabled = false;
                btnAdicionar.Enabled = false;
                btnLimpar.Enabled = false;
                txtDisponivel.Enabled = true;

                txtDisponivel.Clear();
            }
        }
        //Remove candidatos inseridos por engano no DataGridView
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            grdLista.Rows.RemoveAt(grdLista.CurrentRow.Index);
            MessageBox.Show("Candidato removido com sucesso!","Concluído",MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Caso o primeiro candidado inserido não seja o correto, o sistema desabilita os botoes para nova tentativa. Já que o grid está vazio.
            if (lista.Count == 0)
            {
                btnEnviar.Enabled = false;
                btnExcluir.Enabled = false;
            }
        }
        //Validadores, onde o usuário pode fornecer apenas a entrada esperada
        private void txtDisponivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                txtDisponivel.BackColor = Color.Red;
            }
            else
            {
                txtDisponivel.BackColor = Color.White;
            }
        }
        private void txtVaga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled= true;
                txtVaga.BackColor = Color.Red;
            }
            else
            {
                txtVaga.BackColor = Color.White;
            }
        }
        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled= true;
                txtNome.BackColor = Color.Red;
            }
            else
            {
                txtNome.BackColor = Color.White;
            }
        }
        private void txtSobrenome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
                txtSobrenome.BackColor = Color.Red;
            }
            else
            {
                txtSobrenome.BackColor = Color.White;
            }
        }
        private void mskCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                mskCpf.BackColor = Color.Red;
            }
            else
            {
                mskCpf.BackColor = Color.White;
            }
        }
    }
}
