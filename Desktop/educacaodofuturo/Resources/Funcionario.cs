﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace educacaodofuturo.Resources
{
    public class Funcionario
    {
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Cpf { get; set; }
        public string Estado { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Sexo { get; set; }
        public string Telefone { get; set; }
        public string Id { get; set; }
        public string HorarioEntrada { get; set; }
        public string HorarioSaida { get; set; }
        public string SaidaIntervalo { get; set; }
        public string VoltaIntervalo { get; set; }

        private Action<Funcionario> action;
        private Action<bool> actionResult;
        Action<List<Funcionario>> actionRetList;

        public void Login(Action<Funcionario> action)
        {
            this.action = action;
            try
            {
                new Firebase().Login(ResultLogin, LoginErro, Email, Senha);
            }
            catch
            {
                Id = "";
                action(this);
            }
        }

        public void ResultLogin(DocumentSnapshot snapshot)
        {
            Funcionario func = new Funcionario();
            Id = snapshot.Id;
            Email = snapshot.GetValue<string>("email");
            Cpf = snapshot.GetValue<string>("cpf");
            Nome = snapshot.GetValue<string>("nome");
            Cargo = snapshot.GetValue<string>("cargo");
            Sexo = snapshot.GetValue<string>("sexo");
            action(this);
        }

        public void LoginErro(string erro)
        {
            Funcionario func = new Funcionario();
            Id = "";
            action(this);
        }

        public void Cadastrar(Action<bool> result)
        {
            actionResult = result;
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("nome", Nome);
            values.Add("email", Email);
            values.Add("senha", Senha);
            values.Add("cargo", Cargo);
            values.Add("cep", Cep);
            values.Add("bairro", Bairro);
            values.Add("cidade", Cidade);
            values.Add("cpf", Cpf);
            values.Add("estado", Estado);
            values.Add("numero", Numero);
            values.Add("rua", Rua);
            values.Add("sexo", Sexo);
            values.Add("telefone", Telefone);
            new Firebase().CadastrarFuncionario(values, CadastrarResult);
        }

        public void RetProfessores(Action<List<Funcionario>> action)
        {
            this.actionRetList = action;
            new Resources.Firebase().BuscarPorCargo(RetTodosResult, "Funcionarios","Professor");
        }
        public void RetProfessoresResult(QuerySnapshot querySnapshot)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            foreach (var document in querySnapshot)
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Id = document.Id;
                funcionario.Nome = document.GetValue<string>("nome");
                funcionario.Cpf = document.GetValue<string>("cpf");
                funcionario.Cargo = document.GetValue<string>("cargo");
                funcionario.Email = document.GetValue<string>("email");
                funcionario.Telefone = document.GetValue<string>("telefone");
                funcionarios.Add(funcionario);
            }
            actionRetList(funcionarios);
        }

        public void CadastrarResult(bool cadastrado)
        {
            actionResult(cadastrado);
        }

        public void RetTodos(Action<List<Funcionario>> action)
        {
            this.actionRetList = action;
            new Resources.Firebase().BuscarTodos(RetTodosResult, "Funcionarios");
        }
        public void RetTodosResult(QuerySnapshot querySnapshot)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            foreach (var document in querySnapshot)
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Id = document.Id;
                funcionario.Nome = document.GetValue<string>("nome");
                funcionario.Cargo = document.GetValue<string>("cargo");
                funcionario.Email = document.GetValue<string>("email");
                funcionario.Telefone = document.GetValue<string>("telefone");
                funcionarios.Add(funcionario);
            }
            actionRetList(funcionarios);
        }

        public void RetFreq(string campo, string campoValor, Action<List<Funcionario>> action)
        {
            actionRetList = action;
            new Resources.Firebase().RetornarFrequencia("FrequenciaFuncs", campo, campoValor,RetFreqResult);
        }
        public void RetFreqResult(QuerySnapshot querySnapshot)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            foreach (var document in querySnapshot)
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Cpf = document.GetValue<string>("cpf");
                funcionario.HorarioEntrada = document.GetValue<string>("horarioEntrada");
                funcionario.HorarioSaida = document.GetValue<string>("horarioSaida");
                funcionario.SaidaIntervalo = document.GetValue<string>("saidaIntervalo");
                funcionario.VoltaIntervalo = document.GetValue<string>("voltaIntervalo");
                funcionarios.Add(funcionario);
            }
            actionRetList(funcionarios);
        }

        public void RetFreqCargo(string campo, string campoValor, Action<List<Funcionario>> action, string cargo)
        {
            actionRetList = action;
            new Resources.Firebase().RetornarFrequenciaCargo("FrequenciaFuncs", campo, campoValor, cargo, RetFreqCargoResult);
        }
        public void RetFreqCargoResult(QuerySnapshot querySnapshot)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            foreach (var document in querySnapshot)
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Cpf = document.GetValue<string>("cpf");
                funcionario.HorarioEntrada = document.GetValue<string>("horarioEntrada");
                funcionario.HorarioSaida = document.GetValue<string>("horarioSaida");
                funcionario.SaidaIntervalo = document.GetValue<string>("saidaIntervalo");
                funcionario.VoltaIntervalo = document.GetValue<string>("voltaIntervalo");
                funcionarios.Add(funcionario);
            }
            actionRetList(funcionarios);
        }

        public void BuscarFreq(string data, Action<Funcionario> action)
        {
            this.action = action;
            new Resources.Firebase().GetFreq(Cpf, data, Cargo, action);

        }

        public void AlterarFreq(string data, Action<bool> action)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("cpf", Cpf);
            values.Add("data", data);
            values.Add("cargo", Cargo);
            values.Add("horarioEntrada", HorarioEntrada);
            values.Add("horarioSaida", HorarioSaida);
            values.Add("saidaIntervalo", SaidaIntervalo);
            values.Add("voltaIntervalo", VoltaIntervalo);
            new Resources.Firebase().AlterarFreq(Cpf, data, values, action);
        }
    }
}
