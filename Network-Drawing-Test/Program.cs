using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Raylib_cs;

namespace Network_Drawing_Test;
public class Jugador(int x, int y, Color color)
{
  public int x = x;
  public int y = y;
  public Color color = color;
  public void Draw()
  {
    Raylib.DrawRectangle(x, y, 50, 50, color);
  }
}
public class Program
{

  public static List<Jugador> jugadores = new();
  [System.STAThread]
  public static void Main(string[] args)
  {
    ServidorChat server = new(8080);
    _ = server.Main();
    if(args.Length > 0 && args[0].Equals("server"))
    {
      Raylib.InitWindow(Graphics_Globals.WindowWidth, Graphics_Globals.WindowHeight, "Drawing");
      Raylib.InitAudioDevice();

      while(!Raylib.WindowShouldClose())
      {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Raylib.GetColor(Colors.Background));
        foreach(var j in Program.jugadores)
        {
          j.Draw();
        }

        Raylib.EndDrawing();
      }
      Raylib.CloseWindow();
      Raylib.CloseAudioDevice();
    }
    else
    {

    }
  }
}

class ServidorChat
{
  private readonly TcpListener escucha;
  private readonly ConcurrentDictionary<Jugador, TcpClient> clientes = new();

  public ServidorChat(int puerto)
  {
    escucha = new TcpListener(IPAddress.Any, puerto);
  }

  public async Task IniciarAsync()
  {
    escucha.Start();
    while (true)
    {
      var cliente = await escucha.AcceptTcpClientAsync();
      Random r = new();
      Jugador j = new(r.Next(Graphics_Globals.WindowWidth-50), r.Next(Graphics_Globals.WindowHeight-50), Colors.giveRandomColor());
      clientes[j] = cliente;
      _ = ManejarClienteAsync(j, cliente);
    }
  }

  private async Task ManejarClienteAsync(Jugador jugador, TcpClient cliente)
  {
    var flujo = cliente.GetStream();
    var buffer = new byte[1024];
    try
    {
        while (true)
        {
            int leidos = await flujo.ReadAsync(buffer, 0, buffer.Length);
            if (leidos == 0) break;
            string mensaje = Encoding.UTF8.GetString(buffer, 0, leidos);
            if(mensaje.Equals("W"))
            {
              jugador.y -= 5;
            }
            await BroadcastAsync("", jugador);
        }
    }
    finally
    {
        clientes.TryRemove(jugador, out _);
        cliente.Close();
        Console.WriteLine($"Cliente {jugador.ToString()} desconectado.");
    }
  }

  private async Task BroadcastAsync(string mensaje, Jugador j)
  {
    var datos = Encoding.UTF8.GetBytes(mensaje);
    foreach (var kv in clientes)
    {
      if (kv.Key == j) continue;
      try
      {
        var flujo = kv.Value.GetStream();
        await flujo.WriteAsync(datos, 0, datos.Length);
      }
      catch { }
    }
  }

  public async Task Main()
  {
    var servidor = new ServidorChat(5000);
    await servidor.IniciarAsync();
  }
}
