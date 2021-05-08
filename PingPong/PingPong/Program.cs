using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace PingPong
{
    class Program : GameWindow
    {   
        int xDaBola = 0;
        int yDaBola = 0;
        int tamanhoDaBola = 20;
        int velocidadeDaBolaEmX = 5;
        int velocidadeDaBolaEmY = 5;
        int yJogador1 = 0;
        int yJogador2 = 0;

        int xDoJogador1()
        {
            return  -ClientSize.Width /2 + larguraDosJogadores() / 2;
        }
        int xDoJogador2()
        {
            return  ClientSize.Width /2 - larguraDosJogadores() / 2;
        }
        int larguraDosJogadores()
        {
            return tamanhoDaBola;
        }
         int alturaDosJogadores()
        {
            return 3 * tamanhoDaBola;
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            xDaBola = xDaBola + velocidadeDaBolaEmX;
            yDaBola = yDaBola + velocidadeDaBolaEmY;

           if (xDaBola + tamanhoDaBola / 2 > xDoJogador2() - larguraDosJogadores() / 2 
                && yDaBola - tamanhoDaBola / 2 < yJogador2 + alturaDosJogadores() / 2 
                && yDaBola + tamanhoDaBola / 2 > yJogador2 - alturaDosJogadores() / 2)
            {
                velocidadeDaBolaEmX = - velocidadeDaBolaEmX;
            }
           if (xDaBola - tamanhoDaBola / 2 < xDoJogador1() + larguraDosJogadores() / 2 
                && yDaBola - tamanhoDaBola / 2 < yJogador1 + alturaDosJogadores() / 2 
                && yDaBola + tamanhoDaBola / 2 > yJogador1 - alturaDosJogadores() / 2)
            {
                velocidadeDaBolaEmX = - velocidadeDaBolaEmX;
            }

           if (yDaBola + tamanhoDaBola / 2 > ClientSize.Height / 2)
            {
                velocidadeDaBolaEmY = - velocidadeDaBolaEmY ;
            }
           if (yDaBola - tamanhoDaBola / 2 < -ClientSize.Height / 2)
            {
                velocidadeDaBolaEmY = - velocidadeDaBolaEmY;
            }
            if (xDaBola < -ClientSize.Width / 2 || xDaBola > ClientSize.Width / 2 )
            {
                xDaBola = 0;
                yDaBola = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                yJogador1 = yJogador1 + 5;
            }
             if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                yJogador1 = yJogador1 - 5;
            }
             if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                yJogador2 = yJogador2 + 5;
            }
             if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                yJogador2 = yJogador2 - 5;
            }
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DesenharRetangulo(xDaBola, yDaBola, tamanhoDaBola, tamanhoDaBola,0.0f,0.0f,255.0f); //Bolinha
            DesenharRetangulo(xDoJogador1(),yJogador1,larguraDosJogadores(),alturaDosJogadores(),255.0f,215.0f,0.0f);
            DesenharRetangulo(xDoJogador2(),yJogador2,larguraDosJogadores(),alturaDosJogadores(),0.0f,191.0f,255.0f);
            

            SwapBuffers();
        }
        void DesenharRetangulo(int x, int y, int largura, int altura, float r , float g, float b)
        {
            GL.Color3(r,g,b);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End();
        }
        static void Main ()
        {
            new Program().Run();
        }
        
    }
}
