﻿/*
 * Pizzería Campus Express - Gestión de pedidos con Queue y Stack
 * Compatible con SharpDevelop 4.4 / .NET Framework 2.0+
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace laboratoriPizzeriaCampusExpress
{
    public partial class MainForm : Form
    {
        // Colecciones principales: FIFO para pedidos, LIFO para bitácora
        private Queue<string> colaPedidos = new Queue<string>();
        private Stack<string> pilaBitacora = new Stack<string>();
        private Queue<string> colaPedidosPremium = new Queue<string>();

        public MainForm()
        {
            InitializeComponent();
            ActualizarUI();
        }

        // PASO 1: Nuevo pedido (FIFO entrada)
        private void BtnNuevoPedido_Click(object sender, EventArgs e)
        {
            string cliente = txtCliente.Text.Trim();

            // Validar entrada
            if (cliente == "")
            {
                lblEstado.Text = string.Format("⚠️ Debe ingresar un nombre de cliente.");
                return;
            }

            // Agregar a la cola
            colaPedidos.Enqueue(cliente);

            // Registrar en la pila
            pilaBitacora.Push(string.Format("PEDIDO: {0}", cliente));

            // Limpiar campo y actualizar
            txtCliente.Clear();
            lblEstado.Text = string.Format("✅ Pedido registrado para {0}", cliente);
            ActualizarUI();
        }

        // PASO 2: Entregar pedido (FIFO salida)
        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (colaPedidos.Count == 0 && colaPedidosPremium.Count == 0)
            {
                lblEstado.Text = string.Format("❌ No hay pedidos pendientes.");
                return;
            }

            string cliente = "";
            if (colaPedidosPremium.Count > 0) {
            
            cliente = colaPedidosPremium.Dequeue();
            pilaBitacora.Push(string.Format("ENTREGADO PREMIUM: {0}", cliente));
            lblEstado.Text = string.Format("🍕 Pedido PREMIUM entregado a {0}", cliente);
            }
            else{
            
            cliente = colaPedidos.Dequeue();
            pilaBitacora.Push(string.Format("ENTREGADO: {0}", cliente));
            lblEstado.Text = string.Format("🍕 Pedido entregado a {0}", cliente);
            }
            
            ActualizarUI();
        }

        // PASO 3: Deshacer última acción (LIFO + lógica de reversión)
        private void BtnDeshacer_Click(object sender, EventArgs e)
        {
            if (pilaBitacora.Count == 0)
            {
                lblEstado.Text = string.Format("📭 No hay acciones para deshacer.");
                return;
            }

            string ultimaAccion = pilaBitacora.Pop();

            if (ultimaAccion.StartsWith("PEDIDO:"))
            {
                // Extraer nombre del cliente
                string nombre = ultimaAccion.Replace("PEDIDO: ", "").Trim();
                // Reconstruir cola excluyendo ese pedido
                string[] temporal = colaPedidos.ToArray();
                colaPedidos.Clear();
                foreach (string p in temporal)
                {
                    if (p != nombre)
                        colaPedidos.Enqueue(p);
                }
                lblEstado.Text = string.Format("↩️ Se deshizo el pedido de {0}", nombre);
            }
            
            else if (ultimaAccion.StartsWith("PREMIUM:"))
			{
    				string nombre = ultimaAccion.Replace("PREMIUM: ", "").Trim();
   					string[] temporal = colaPedidosPremium.ToArray();
    				colaPedidosPremium.Clear();
    				foreach (string p in temporal)
    				{
        			if (p != nombre)
            		colaPedidosPremium.Enqueue(p);
   				 	}
    			lblEstado.Text = string.Format("↩️ Se deshizo el pedido premium de {0}", nombre);
			}
            
            else if (ultimaAccion.StartsWith("ENTREGADO:"))
            {
                // Extraer nombre del cliente
                string nombre = ultimaAccion.Replace("ENTREGADO: ", "").Trim();
                // Volver a encolar
                colaPedidos.Enqueue(nombre);
                lblEstado.Text = string.Format("↩️ Se deshizo la entrega a {0}", nombre);
            }
            
            else if (ultimaAccion.StartsWith("ENTREGADO PREMIUM:"))
            {
                string nombre = ultimaAccion.Replace("ENTREGADO PREMIUM: ", "").Trim();
                colaPedidosPremium.Enqueue(nombre);
                lblEstado.Text = string.Format("↩️ Se deshizo la entrega premium a {0}", nombre);
            }
  
            else
            {
                lblEstado.Text = string.Format("⚠️ Acción desconocida en bitácora.");
            }

            ActualizarUI();
        }


        // PASO 4: Limpiar todo (reiniciar sistema)
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            colaPedidos.Clear();
            pilaBitacora.Clear();
            colaPedidosPremium.Clear();
            lblEstado.Text = string.Format("🧹 Sistema reiniciado.");
            ActualizarUI();
        }

        // Sincronizar la interfaz con el estado actual
        private void ActualizarUI()
        {
            // Limpiar listas visuales
            lstPedidos.Items.Clear();
            lstBitacora.Items.Clear();
            //mostrar cola de pedidos premium
            foreach (string p in colaPedidosPremium) 
            	lstPedidos.Items.Add(p);

            // Mostrar cola de pedidos
            foreach (string p in colaPedidos)
                lstPedidos.Items.Add(p);
            if (colaPedidos.Count == 0 && colaPedidosPremium.Count == 0)
                lstPedidos.Items.Add("(Sin pedidos pendientes)");

            // Mostrar bitácora (pila)
            foreach (string accion in pilaBitacora)
                lstBitacora.Items.Add(accion);
            if (pilaBitacora.Count == 0)
                lstBitacora.Items.Add("(Sin acciones registradas)");

            // Actualizar contador
            int totalpedidos = colaPedidosPremium.Count + colaPedidos.Count;
            lblContador.Text = string.Format("Pedidos: {0} | Bitácora: {1}",
                totalpedidos, pilaBitacora.Count);
        }
        
        void BtnPedidoPremiumClick(object sender, EventArgs e)
        {
			string cliente = txtCliente.Text.Trim();

            // Validar entrada
            if (cliente == "")
            {
                lblEstado.Text = string.Format("⚠️ Debe ingresar un nombre de cliente.");
                return;
            }

            // Agregar a la cola premium
            colaPedidosPremium.Enqueue(cliente);

            // Registrar en la pila como PREMIUM
            pilaBitacora.Push(string.Format("PREMIUM: {0}", cliente));

            // Limpiar campo y actualizar
            txtCliente.Clear();
            lblEstado.Text = string.Format("✅ Pedido PREMIUM registrado para {0}", cliente);
            ActualizarUI();        	
        }
    }
}