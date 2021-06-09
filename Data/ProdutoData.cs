﻿using Ecommerce2021a.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce2021a.Data
{
    //CRUD = Create, Read, Update, Delete
    public class ProdutoData : Data
    {
        //métodos ára manipular a tablea Produto do banco de dados
        //Create = Insert

        public void Create(Produto produto)
        {
            //criando cmd (comando) para executar query SQL
            SqlCommand cmd = new SqlCommand();
            //Conexão com o BD
            cmd.Connection = base.connectionDB;
            //Comando SQL para ser executado no BD
            cmd.CommandText = @"Insert into Produtos Values(@nome, @desc, @valor, @idCategoria)";

            cmd.Parameters.AddWithValue("@nome", produto.Nome);
            cmd.Parameters.AddWithValue("@desc", produto.Descricao);
            cmd.Parameters.AddWithValue("@valor", produto.Valor);
            cmd.Parameters.AddWithValue("@idCategoria", produto.IdCategoria);

            cmd.ExecuteNonQuery();
        }

        //Read = Select

        public List<Produto> Read()
        {
            List<Produto> lista = new List<Produto>();

            //criando cmd (comando) para exucetar query SQL
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = base.connectionDB;
            cmd.CommandText = @"Select * From v_produto";

            //execução do Select no BD
            //o reader vai receber o retorno do Select
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())//percorrendo a table até o EOF (End of File)
            {
                Produto produto = new Produto();

                produto.IdProduto = (int)reader["IdProduto"];
                produto.Nome = (string)reader["Nome"];
                produto.Descricao = (string)reader["Descricao"];
                produto.Valor = (decimal)reader["Valor"];
                produto.IdCategoria = (int)reader["IdCategoria"];
                produto.Categoria = (string)reader["Categoria"];

                lista.Add(produto);
            }


            return lista;
        }

        public Produto Read(int id)
        {
            Produto produto = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = base.connectionDB;
            cmd.CommandText = @"Select * From Produtos Where IdProduto = @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                produto = new Produto
                {
                    IdProduto = (int)reader["IdProduto"],
                    Nome = (string)reader["Nome"],
                    Descricao = (string)reader["Descricao"],
                    Valor = (decimal)reader["Valor"],
                    IdCategoria = (int)reader["IdCategoria"]
                };
            }

            return produto;
        }

        public void Update(Produto produto)
        {
            //criado o cmd (comando SQl) para executar um comando SQL no Banco de Dados
            SqlCommand cmd = new SqlCommand();
            //conexão com o Banco de Dados
            cmd.Connection = base.connectionDB;
            //criada a String SQL
            cmd.CommandText = @"Update Produtos set Nome = @nome, Descricao = @desc, Valor = @valor, IdCategoria = @idC
                                Where IdProduto = @id";

            cmd.Parameters.AddWithValue("@id", produto.IdProduto);
            cmd.Parameters.AddWithValue("@nome", produto.Nome);
            cmd.Parameters.AddWithValue("@desc", produto.Descricao);
            cmd.Parameters.AddWithValue("@valor", produto.Valor);
            cmd.Parameters.AddWithValue("@idC", produto.IdCategoria);

            cmd.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            try
            {
                //criado o cmd (comando SQl) para executar um comando SQL no Banco de Dados
                SqlCommand cmd = new SqlCommand();
                //conexão com o Banco de Dados
                cmd.Connection = base.connectionDB;
                //criada a String SQL
                cmd.CommandText = @"Delete from Produtos Where IdProduto = @id";

                cmd.Parameters.AddWithValue("@id", id);

                //Execução do comando Delete no Banco de Dados
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Pode haver uma compra atrelada ao ID deste Produto \n\n" + ex);

                return false;
            }


        }

        }
    }
