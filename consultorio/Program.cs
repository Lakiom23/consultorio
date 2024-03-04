using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consultorio
{
    class Program
    {

        // Clase Persona
        public class Persona
        {
            private string nombre;
            private string apellido;
            private int edad;
            private int cedula;


            public Persona(string nombre, string apellido, int edad, int cedula)
            {
                this.nombre = nombre;
                this.apellido = apellido;
                this.edad = edad;
                this.cedula = cedula;
            }


            public string Nombre
            {
                get { return nombre; }
                set { nombre = value; }
            }

            public string Apellido
            {
                get { return apellido; }
                set { apellido = value; }
            }

            public int Edad
            {
                get { return edad; }
                set { edad = value; }
            }

            public int Cedula
            {
                get { return cedula; }
                set { cedula = value; }
            }

        }

        //Clase Paciente
        public class Paciente : Persona
        {
            private int prioridad;

            public Paciente(string nombre, string apellido, int edad, int cedula, int prioridad) : base(nombre, apellido, edad, cedula)
            {
                this.prioridad = prioridad;
            }

            public int Prioridad
            {
                get { return prioridad; }
                set { prioridad = value; }
            }
        }

        public class Medico : Persona
        {
            private string especialidad;
            private Paciente paciente;

            public Medico(string nombre, string apellido, int edad, int cedula, string especialidad) : base(nombre, apellido, edad, cedula)
            {
                this.especialidad = especialidad;
                this.paciente = null;
            }

            public string Especialidad1
            {
                get { return especialidad; }
                set { especialidad = value; }
            }

            internal Paciente Paciente
            {
                get { return paciente; }
                set { paciente = value; }
            }

            public void AtenderPaciente(Paciente paciente)
            {
                if (this.paciente == null)
                {
                    this.paciente = paciente;
                    Console.WriteLine("Paciente ha entrado al consultorio");
                }
                else
                {
                    Console.WriteLine("El medico se encuentra ocupado con otro paciente");
                }
            }

            public void PacienteAtendido()
            {
                if (this.paciente != null)
                {
                    this.paciente = null;
                    Console.WriteLine("Paciente ha sido atendido, medico disponible");
                }
                else
                {
                    Console.WriteLine("No hay paciente siendo atendido, el medico se encuentra disponible");
                }
            }

            public void MostrarPaciente()
            {
                if (Paciente != null)
                {
                    Console.WriteLine("Paciente siendo atendido ");
                    Console.WriteLine($"Nombre: {Paciente.Nombre} ");
                    Console.WriteLine($"Apellido: {Paciente.Apellido} ");
                    Console.WriteLine($"Edad: {Paciente.Edad} ");
                    Console.WriteLine($"C.I: {Paciente.Cedula} ");
                }
                else
                {
                    Console.WriteLine("No hay paciente siendo atendido, el medico se encuentra disponible");
                }
            }
        }

        //Nodo Paciente
        public class NodoPaciente
        {
            private Paciente paciente;
            private NodoPaciente siguiente;

            public NodoPaciente(Paciente paciente)
            {
                this.paciente = paciente;
                this.siguiente = null;
            }

            internal Paciente Paciente { get => paciente; set => paciente = value; }
            internal NodoPaciente Siguiente { get => siguiente; set => siguiente = value; }
        }

        //ColaPacientes
        public class ColaPacientes
        {
            private NodoPaciente inicio;
            private NodoPaciente fin;
            private int contador;
            private int tamanio;

            internal NodoPaciente Fin { get => fin; set => fin = value; }
            internal NodoPaciente Inicio { get => inicio; set => inicio = value; }
            public int Contador { get => contador; set => contador = value; }
            public int Tamanio { get => tamanio; set => tamanio = value; }

            public ColaPacientes()
            {
                Tamanio = 10;
                Contador = 0;
            }
            public bool vacia()
            {
                return Inicio == null;
            }
            public bool llena()
            {

                return Contador >= Tamanio;
            }

            //ingresar paciente nuevo 
            public void Push(Paciente paciente)
            {

                NodoPaciente nuevoPaciente = new NodoPaciente(paciente);
                if (llena())
                {
                    Console.WriteLine("Cola Llena, de momento no se puede ingresar mas pacientes en colsultorio");
                    return;
                }
                if (vacia())
                {
                    Inicio = nuevoPaciente;
                    Fin = nuevoPaciente;

                    Console.WriteLine("Paciente agregado a la Cola");
                    return;

                }
                else
                {
                    Fin.Siguiente = nuevoPaciente;
                    Fin = nuevoPaciente;
                }

                Contador++;
            }
            //metodo para atender al paciente 
            public Paciente pop()
            {
                if (vacia())
                {
                    Console.WriteLine("La Cola esta vacia no hay nadie aquien atender ");
                    return null;
                }
                // verificar si hace falata eliminar ya que no estoy seguro si se trabaja igual que con punteros

                Paciente Atender = Inicio.Paciente;
                Reorganizar();

                Contador--;
                return Atender;
            }

            //reorganiza la cola una vez atendido un paciente 
            public void Reorganizar()
            {
                Inicio = Inicio.Siguiente;
                if (vacia()) fin = null;
            }

            public void Mostra()
            {
                NodoPaciente nuevo = inicio;
                if (vacia())
                {
                    Console.WriteLine("Actualmente no hay pacientes en esta cola");
                }
                else
                {
                    while (nuevo != null)
                    {
                        Console.WriteLine("Pacientes en cola ");
                        Console.WriteLine($"Nombre : {nuevo.Paciente.Nombre} ");
                        Console.WriteLine($"Apellido : {nuevo.Paciente.Apellido} ");
                        Console.WriteLine($"Edad : {nuevo.Paciente.Edad} ");
                        Console.WriteLine($"C.I : {nuevo.Paciente.Cedula} ");
                        nuevo = nuevo.Siguiente;
                    }
                }


            }



        }

        //Class Hospital
        public class Hospital
        {
            private ColaPacientes[] adultos;
            private ColaPacientes[] ninios;
            private Medico general;
            private Medico pediatra;

            //Constructor
            public Hospital()
            {
                for (int i = 0; i > 5; i++)
                {
                    adultos[i] = new ColaPacientes();
                    ninios[i] = new ColaPacientes();
                    general = null;
                    pediatra = null;
                }
            }

            //Getters y Setters
            internal Medico General { get => General; set => General = value; }
            internal Medico Pediatra { get => Pediatra; set => Pediatra = value; }
            internal ColaPacientes[] Adultos { get => adultos; set => adultos = value; }
            internal ColaPacientes[] Ninios { get => Ninios; set => Ninios = value; }


            public void InsertarPaciente()
            {
                Paciente nuevo;
                Random random = new Random();
                int prioridad = random.Next(0, 6);
                int edad = random.Next(1, 100);

                Console.WriteLine("Ingrese los datos del Paciente ");
                Console.WriteLine("Nombre : ");
                Console.WriteLine("Apellido : ");
                Console.WriteLine("C.I : ");
                Console.WriteLine("Edad : ");

            }


            public void AtenderPaciente()
            {



            }

            public void GenerarReporte()
            {

            }


            public void Menu()
            {
                int opcion = 0;

                do
                {
                    Console.WriteLine("Consultorio medico ");
                    Console.WriteLine("1.Nuevo Paciente");
                    Console.WriteLine("2.Atender Pacientes");
                    Console.WriteLine("3.Generar reporte ");
                    Console.WriteLine("4.Cambio de doctor");
                    Console.WriteLine("5.CERRAR SISTEMA ");

                    opcion = Convert.ToByte(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            InsertarPaciente();
                            Console.ReadKey();
                            break;

                        case 2:
                            AtenderPaciente();
                            Console.ReadKey();
                            break;

                        case 3:
                            GenerarReporte();
                            Console.ReadKey();
                            break;
                    }

                } while (opcion != 4);

            }

        }




        static void Main(string[] args)
        {
            Hospital consultorio = new Hospital();
            consultorio.Menu();

            Console.ReadKey();
        }
    }
}
