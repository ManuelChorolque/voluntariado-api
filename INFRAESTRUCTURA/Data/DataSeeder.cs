using DOMINIO.Entidades;
using DOMINIO.Enumeradores;

namespace Infraestructura.Data
{
    public class DataSeeder
    {
        private const string PasswordHash = "$2a$11$j9bdBGDdYkevHgEVxn8Giuo4bP03v.m/SQuRjdxUWSRMHwG6ArfHW";

        public static async Task SeedAsync(VoluntariadoDbContext contexto)
        {
            if (contexto.Usuarios.Any())
                return;

            var fechaBase = DateTime.UtcNow;

            // Organizaciones
            var org1 = new Organizacion
            {
                Nombre = "Fundaci\u00f3n Esperanza",
                Descripcion = "ONG dedicada a la educaci\u00f3n infantil en comunidades vulnerables",
                Contacto = "Mar\u00eda L\u00f3pez",
                Email = "contacto@fundacionesperanza.org",
                Telefono = "+591 72210001",
                Direccion = "Av. Siempre Viva 123, La Paz",
                FechaRegistro = fechaBase.AddMonths(-6)
            };

            var org2 = new Organizacion
            {
                Nombre = "EcoVida Bolivia",
                Descripcion = "Proyectos de reforestaci\u00f3n y conservaci\u00f3n ambiental",
                Contacto = "Carlos Mendoza",
                Email = "info@ecovida.bo",
                Telefono = "+591 72210002",
                Direccion = "Calle Naturaleza 456, Cochabamba",
                FechaRegistro = fechaBase.AddMonths(-4)
            };

            var org3 = new Organizacion
            {
                Nombre = "Manos Solidarias",
                Descripcion = "Apoyo a adultos mayores y centros de acogida",
                Contacto = "Ana R\u00edos",
                Email = "contacto@manossolidarias.org",
                Telefono = "+591 72210003",
                Direccion = "Av. Solidaridad 789, Santa Cruz",
                FechaRegistro = fechaBase.AddMonths(-2)
            };

            contexto.Organizaciones.AddRange(org1, org2, org3);
            await contexto.SaveChangesAsync();

            // Voluntarios
            var vol1 = new Voluntario
            {
                Nombre = "Pedro",
                Apellido = "Garc\u00eda",
                Email = "pedro.garcia@email.com",
                Telefono = "+591 71110001",
                Direccion = "Av. Bol\u00edvar 100",
                Cedula = "1234567LP",
                FechaNacimiento = new DateTime(1995, 5, 15),
                FechaRegistro = fechaBase.AddMonths(-5),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 55,
                OrganizacionId = org1.Id
            };

            var vol2 = new Voluntario
            {
                Nombre = "Luc\u00eda",
                Apellido = "Mamani",
                Email = "lucia.mamani@email.com",
                Telefono = "+591 71110002",
                Direccion = "Calle 10 Nro 200",
                Cedula = "7654321LP",
                FechaNacimiento = new DateTime(1998, 8, 22),
                FechaRegistro = fechaBase.AddMonths(-4),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 25,
                OrganizacionId = org1.Id
            };

            var vol3 = new Voluntario
            {
                Nombre = "Jorge",
                Apellido = "Quispe",
                Email = "jorge.quispe@email.com",
                Telefono = "+591 71110003",
                Direccion = "Av. Ecol\u00f3gica 500",
                Cedula = "9876543CB",
                FechaNacimiento = new DateTime(2000, 1, 10),
                FechaRegistro = fechaBase.AddMonths(-3),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 15,
                OrganizacionId = org2.Id
            };

            var vol4 = new Voluntario
            {
                Nombre = "Sof\u00eda",
                Apellido = "Paredes",
                Email = "sofia.paredes@email.com",
                Telefono = "+591 71110004",
                Direccion = "Calle Amistad 300",
                Cedula = "4567890SC",
                FechaNacimiento = new DateTime(2002, 11, 3),
                FechaRegistro = fechaBase.AddMonths(-2),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 10,
                OrganizacionId = org3.Id
            };

            var vol5 = new Voluntario
            {
                Nombre = "Diego",
                Apellido = "Morales",
                Email = "diego.morales@email.com",
                Telefono = "+591 71110005",
                Direccion = "Av. Principal 777",
                Cedula = "3216547LP",
                FechaNacimiento = new DateTime(1993, 7, 19),
                FechaRegistro = fechaBase.AddMonths(-1),
                Estado = EstadoVoluntario.Activo,
                HorasTotales = 0,
                OrganizacionId = null
            };

            contexto.Voluntarios.AddRange(vol1, vol2, vol3, vol4, vol5);
            await contexto.SaveChangesAsync();

            // === ACTIVIDADES (20+ con todos los estados) ===
            var hoy = fechaBase;

            // --- Planificadas (4) ---
            var act1 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Feria Educativa Comunitaria",
                Descripcion = "Organizaci\u00f3n y ejecuci\u00f3n de una feria educativa para ni\u00f1os y j\u00f3venes de la comunidad",
                FechaInicio = hoy.AddDays(30),
                FechaFin = hoy.AddDays(32),
                Ubicacion = "Plaza Principal, La Paz",
                VoluntariosRequeridos = 10,
                Estado = EstadoActividad.Planificada,
                FechaRegistro = hoy.AddDays(-2)
            };
            var act2 = new Actividad
            {
                OrganizacionId = org2.Id,
                Nombre = "Reforestaci\u00f3n Urbana",
                Descripcion = "Plantaci\u00f3n de \u00e1rboles en \u00e1reas urbanas degradadas",
                FechaInicio = hoy.AddDays(45),
                FechaFin = hoy.AddDays(46),
                Ubicacion = "Zona Sur, Cochabamba",
                VoluntariosRequeridos = 25,
                Estado = EstadoActividad.Planificada,
                FechaRegistro = hoy.AddDays(-5)
            };
            var act3 = new Actividad
            {
                OrganizacionId = org3.Id,
                Nombre = "Taller de Cocina para Adultos Mayores",
                Descripcion = "Taller gastron\u00f3mico recreativo para adultos mayores del hogar",
                FechaInicio = hoy.AddDays(20),
                FechaFin = hoy.AddDays(24),
                Ubicacion = "Hogar San Jos\u00e9, Santa Cruz",
                VoluntariosRequeridos = 6,
                Estado = EstadoActividad.Planificada,
                FechaRegistro = hoy.AddDays(-3)
            };
            var act4 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Campa\u00f1a de Vacunaci\u00f3n",
                Descripcion = "Apoyo en la campa\u00f1a de vacunaci\u00f3n infantil en comunidades rurales",
                FechaInicio = hoy.AddDays(60),
                FechaFin = hoy.AddDays(65),
                Ubicacion = "Comunidad Alto Beni",
                VoluntariosRequeridos = 12,
                Estado = EstadoActividad.Planificada,
                FechaRegistro = hoy.AddDays(-1)
            };

            // --- Abiertas (8) ---
            var act5 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Clases de Apoyo Escolar",
                Descripcion = "Refuerzo escolar en matem\u00e1ticas y lenguaje para ni\u00f1os de primaria",
                FechaInicio = hoy.AddDays(10),
                FechaFin = hoy.AddDays(40),
                Ubicacion = "Centro Comunitario Villa Esperanza",
                VoluntariosRequeridos = 5,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = hoy.AddDays(-15)
            };
            var act6 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Campa\u00f1a de \u00datiles Escolares",
                Descripcion = "Recolecci\u00f3n y distribuci\u00f3n de \u00fatiles para ni\u00f1os de escasos recursos",
                FechaInicio = hoy.AddDays(5),
                FechaFin = hoy.AddDays(20),
                Ubicacion = "Oficinas Centrales Fundaci\u00f3n Esperanza",
                VoluntariosRequeridos = 8,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = hoy.AddDays(-20)
            };
            var act7 = new Actividad
            {
                OrganizacionId = org3.Id,
                Nombre = "Taller de Manualidades para Adultos Mayores",
                Descripcion = "Taller recreativo para adultos mayores en el hogar de acogida",
                FechaInicio = hoy.AddDays(7),
                FechaFin = hoy.AddDays(28),
                Ubicacion = "Hogar San Jos\u00e9",
                VoluntariosRequeridos = 4,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = hoy.AddDays(-8)
            };
            var act8 = new Actividad
            {
                OrganizacionId = org2.Id,
                Nombre = "Reciclaje Comunitario",
                Descripcion = "Taller de reciclaje y concientizaci\u00f3n ambiental para vecinos",
                FechaInicio = hoy.AddDays(12),
                FechaFin = hoy.AddDays(13),
                Ubicacion = "Centro Cultural, Cochabamba",
                VoluntariosRequeridos = 10,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = hoy.AddDays(-6)
            };
            var act9 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Apoyo en Refugio de Animales",
                Descripcion = "Cuidado y limpieza de animales en el refugio municipal",
                FechaInicio = hoy.AddDays(3),
                FechaFin = hoy.AddDays(17),
                Ubicacion = "Refugio Municipal, La Paz",
                VoluntariosRequeridos = 6,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = hoy.AddDays(-10)
            };
            var act10 = new Actividad
            {
                OrganizacionId = org2.Id,
                Nombre = "Huertos Urbanos Familiares",
                Descripcion = "Ense\u00f1anza de t\u00e9cnicas de cultivo en espacios reducidos",
                FechaInicio = hoy.AddDays(8),
                FechaFin = hoy.AddDays(22),
                Ubicacion = "Centro Agroecol\u00f3gico, Cochabamba",
                VoluntariosRequeridos = 7,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = hoy.AddDays(-4)
            };
            var act11 = new Actividad
            {
                OrganizacionId = org3.Id,
                Nombre = "Alfabetizaci\u00f3n Digital",
                Descripcion = "Ense\u00f1anza b\u00e1sica de computaci\u00f3n para adultos mayores",
                FechaInicio = hoy.AddDays(14),
                FechaFin = hoy.AddDays(35),
                Ubicacion = "Biblioteca Municipal, Santa Cruz",
                VoluntariosRequeridos = 5,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = hoy.AddDays(-7)
            };
            var act12 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Roperito Comunitario",
                Descripcion = "Clasificaci\u00f3n y entrega de ropa donada para familias de bajos recursos",
                FechaInicio = hoy.AddDays(6),
                FechaFin = hoy.AddDays(8),
                Ubicacion = "Parroquia San Mart\u00edn, La Paz",
                VoluntariosRequeridos = 8,
                Estado = EstadoActividad.Abierta,
                FechaRegistro = hoy.AddDays(-3)
            };

            // --- EnProgreso (3) ---
            var act13 = new Actividad
            {
                OrganizacionId = org2.Id,
                Nombre = "Monitoreo de Aves",
                Descripcion = "Registro y monitoreo de aves migratorias en el humedal",
                FechaInicio = hoy.AddDays(-2),
                FechaFin = hoy.AddDays(5),
                Ubicacion = "Humedal Alalay",
                VoluntariosRequeridos = 8,
                Estado = EstadoActividad.EnProgreso,
                FechaRegistro = hoy.AddDays(-14)
            };
            var act14 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Distribuci\u00f3n de Alimentos",
                Descripcion = "Reparto de canastas b\u00e1sicas a familias en situaci\u00f3n de vulnerabilidad",
                FechaInicio = hoy.AddDays(-1),
                FechaFin = hoy.AddDays(3),
                Ubicacion = "Distrito 5, El Alto",
                VoluntariosRequeridos = 10,
                Estado = EstadoActividad.EnProgreso,
                FechaRegistro = hoy.AddDays(-12)
            };
            var act15 = new Actividad
            {
                OrganizacionId = org3.Id,
                Nombre = "Visitas a Centros de Acogida",
                Descripcion = "Acompa\u00f1amiento y actividades recreativas en centros de acogida",
                FechaInicio = hoy.AddDays(-3),
                FechaFin = hoy.AddDays(4),
                Ubicacion = "Centro de Acogida San Miguel",
                VoluntariosRequeridos = 6,
                Estado = EstadoActividad.EnProgreso,
                FechaRegistro = hoy.AddDays(-15)
            };

            // --- Completadas (3) ---
            var act16 = new Actividad
            {
                OrganizacionId = org2.Id,
                Nombre = "Limpieza de R\u00edos",
                Descripcion = "Jornada de limpieza y concientizaci\u00f3n sobre el cuidado del r\u00edo",
                FechaInicio = hoy.AddDays(-5),
                FechaFin = hoy.AddDays(-4),
                Ubicacion = "R\u00edo Rocha",
                VoluntariosRequeridos = 15,
                Estado = EstadoActividad.Completada,
                FechaRegistro = hoy.AddDays(-30)
            };
            var act17 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Jornada M\u00e9dica Gratuita",
                Descripcion = "Apoyo en la organizaci\u00f3n de atenci\u00f3n m\u00e9dica gratuita para la comunidad",
                FechaInicio = hoy.AddDays(-10),
                FechaFin = hoy.AddDays(-9),
                Ubicacion = "Hospital de Cl\u00ednicas, La Paz",
                VoluntariosRequeridos = 20,
                Estado = EstadoActividad.Completada,
                FechaRegistro = hoy.AddDays(-40)
            };
            var act18 = new Actividad
            {
                OrganizacionId = org3.Id,
                Nombre = "Kerm\u00e9s Solidaria",
                Descripcion = "Organizaci\u00f3n de kerm\u00e9s para recaudar fondos para el hogar de ancianos",
                FechaInicio = hoy.AddDays(-8),
                FechaFin = hoy.AddDays(-7),
                Ubicacion = "Parque Urbano, Santa Cruz",
                VoluntariosRequeridos = 12,
                Estado = EstadoActividad.Completada,
                FechaRegistro = hoy.AddDays(-25)
            };

            // --- Canceladas (2) ---
            var act19 = new Actividad
            {
                OrganizacionId = org2.Id,
                Nombre = "Ciclismo por el Medio Ambiente",
                Descripcion = "Recorrido cicl\u00edstico para promover el transporte ecol\u00f3gico (suspendido por lluvias)",
                FechaInicio = hoy.AddDays(-15),
                FechaFin = hoy.AddDays(-14),
                Ubicacion = "Circuito Metropolitano, Cochabamba",
                VoluntariosRequeridos = 30,
                Estado = EstadoActividad.Cancelada,
                FechaRegistro = hoy.AddDays(-35)
            };
            var act20 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Curso de Primeros Auxilios",
                Descripcion = "Capacitaci\u00f3n en primeros auxilios para voluntarios (cancelado por falta de instructores)",
                FechaInicio = hoy.AddDays(-20),
                FechaFin = hoy.AddDays(-18),
                Ubicacion = "Casa de la Cultura, La Paz",
                VoluntariosRequeridos = 15,
                Estado = EstadoActividad.Cancelada,
                FechaRegistro = hoy.AddDays(-50)
            };

            // --- Cerradas (2) ---
            var act21 = new Actividad
            {
                OrganizacionId = org1.Id,
                Nombre = "Campa\u00f1a de Donaci\u00f3n de Sangre",
                Descripcion = "Jornada de donaci\u00f3n de sangre en colaboraci\u00f3n con el banco de sangre municipal",
                FechaInicio = hoy.AddDays(1),
                FechaFin = hoy.AddDays(2),
                Ubicacion = "Banco de Sangre Municipal, La Paz",
                VoluntariosRequeridos = 4,
                Estado = EstadoActividad.Cerrada,
                FechaRegistro = hoy.AddDays(-20)
            };
            var act22 = new Actividad
            {
                OrganizacionId = org3.Id,
                Nombre = "Clases de Ingl\u00e9s para J\u00f3venes",
                Descripcion = "Clases gratuitas de ingl\u00e9s b\u00e1sico para j\u00f3venes de escasos recursos",
                FechaInicio = hoy.AddDays(2),
                FechaFin = hoy.AddDays(30),
                Ubicacion = "Centro Juvenil Esperanza, Santa Cruz",
                VoluntariosRequeridos = 3,
                Estado = EstadoActividad.Cerrada,
                FechaRegistro = hoy.AddDays(-18)
            };

            contexto.Actividades.AddRange(act1, act2, act3, act4, act5, act6, act7, act8, act9, act10,
                                          act11, act12, act13, act14, act15, act16, act17, act18, act19, act20,
                                          act21, act22);
            await contexto.SaveChangesAsync();

            // === ASIGNACIONES ===
            // vol1 (Pedro - el usuario vol@voluntariado.com) -> asignado a algunas, libre en otras
            act1.VoluntariosAsignados.Add(vol1); // Planificada
            act5.VoluntariosAsignados.Add(vol1); // Abierta (ya estaba)
            act6.VoluntariosAsignados.Add(vol1); // Abierta (ya estaba)
            act14.VoluntariosAsignados.Add(vol1); // EnProgreso
            act16.VoluntariosAsignados.Add(vol1); // Completada
            act21.VoluntariosAsignados.Add(vol1); // Cerrada

            // vol2 (Lucía)
            act5.VoluntariosAsignados.Add(vol2); // Abierta
            act9.VoluntariosAsignados.Add(vol2); // Abierta
            act13.VoluntariosAsignados.Add(vol2); // EnProgreso
            act15.VoluntariosAsignados.Add(vol2); // EnProgreso
            act16.VoluntariosAsignados.Add(vol2); // Completada

            // vol3 (Jorge)
            act1.VoluntariosAsignados.Add(vol3); // Planificada
            act8.VoluntariosAsignados.Add(vol3); // Abierta
            act13.VoluntariosAsignados.Add(vol3); // EnProgreso
            act16.VoluntariosAsignados.Add(vol3); // Completada
            act17.VoluntariosAsignados.Add(vol3); // Completada

            // vol4 (Sofía)
            act7.VoluntariosAsignados.Add(vol4); // Abierta
            act10.VoluntariosAsignados.Add(vol4); // Abierta
            act15.VoluntariosAsignados.Add(vol4); // EnProgreso
            act18.VoluntariosAsignados.Add(vol4); // Completada
            act22.VoluntariosAsignados.Add(vol4); // Cerrada

            // vol5 (Diego)
            act7.VoluntariosAsignados.Add(vol5); // Abierta
            act11.VoluntariosAsignados.Add(vol5); // Abierta
            act18.VoluntariosAsignados.Add(vol5); // Completada

            await contexto.SaveChangesAsync();

            // === HORAS REGISTRADAS ===
            var horasList = new List<HorasVoluntariado>
            {
                // vol1
                new() { VoluntarioId = vol1.Id, ActividadId = act14.Id, FechaInicio = act14.FechaInicio, FechaFin = act14.FechaInicio.AddHours(4), Horas = 4, FechaActividad = act14.FechaInicio, FechaRegistro = act14.FechaRegistro, Descripcion = "Reparto de canastas en la zona norte" },
                new() { VoluntarioId = vol1.Id, ActividadId = act16.Id, FechaInicio = act16.FechaInicio, FechaFin = act16.FechaFin, Horas = 8, FechaActividad = act16.FechaInicio, FechaRegistro = act16.FechaRegistro, Descripcion = "Participaci\u00f3n en limpieza de r\u00edos" },
                new() { VoluntarioId = vol1.Id, ActividadId = act21.Id, FechaInicio = act21.FechaInicio, FechaFin = act21.FechaInicio.AddHours(3), Horas = 3, FechaActividad = act21.FechaInicio, FechaRegistro = act21.FechaRegistro, Descripcion = "Donaci\u00f3n y apoyo log\u00edstico" },
                // vol2
                new() { VoluntarioId = vol2.Id, ActividadId = act13.Id, FechaInicio = act13.FechaInicio, FechaFin = act13.FechaInicio.AddHours(6), Horas = 6, FechaActividad = act13.FechaInicio, FechaRegistro = act13.FechaRegistro, Descripcion = "Monitoreo de aves en el humedal" },
                new() { VoluntarioId = vol2.Id, ActividadId = act16.Id, FechaInicio = act16.FechaInicio, FechaFin = act16.FechaFin, Horas = 6, FechaActividad = act16.FechaInicio, FechaRegistro = act16.FechaRegistro, Descripcion = "Apoyo en clasificaci\u00f3n de residuos" },
                // vol3
                new() { VoluntarioId = vol3.Id, ActividadId = act13.Id, FechaInicio = act13.FechaInicio, FechaFin = act13.FechaInicio.AddHours(8), Horas = 8, FechaActividad = act13.FechaInicio, FechaRegistro = act13.FechaRegistro, Descripcion = "Registro de especies migratorias" },
                new() { VoluntarioId = vol3.Id, ActividadId = act16.Id, FechaInicio = act16.FechaInicio, FechaFin = act16.FechaFin, Horas = 8, FechaActividad = act16.FechaInicio, FechaRegistro = act16.FechaRegistro, Descripcion = "Coordinaci\u00f3n de grupos de limpieza" },
                new() { VoluntarioId = vol3.Id, ActividadId = act17.Id, FechaInicio = act17.FechaInicio, FechaFin = act17.FechaFin, Horas = 10, FechaActividad = act17.FechaInicio, FechaRegistro = act17.FechaRegistro, Descripcion = "Organizaci\u00f3n de triaje y registros" },
                // vol4
                new() { VoluntarioId = vol4.Id, ActividadId = act15.Id, FechaInicio = act15.FechaInicio, FechaFin = act15.FechaInicio.AddHours(4), Horas = 4, FechaActividad = act15.FechaInicio, FechaRegistro = act15.FechaRegistro, Descripcion = "Actividades recreativas con adultos mayores" },
                new() { VoluntarioId = vol4.Id, ActividadId = act18.Id, FechaInicio = act18.FechaInicio, FechaFin = act18.FechaFin, Horas = 6, FechaActividad = act18.FechaInicio, FechaRegistro = act18.FechaRegistro, Descripcion = "Atenci\u00f3n en stands de kerm\u00e9s" },
                new() { VoluntarioId = vol4.Id, ActividadId = act22.Id, FechaInicio = act22.FechaInicio, FechaFin = act22.FechaInicio.AddHours(2), Horas = 2, FechaActividad = act22.FechaInicio, FechaRegistro = act22.FechaRegistro, Descripcion = "Clase introductoria de ingl\u00e9s" },
                // vol5
                new() { VoluntarioId = vol5.Id, ActividadId = act18.Id, FechaInicio = act18.FechaInicio, FechaFin = act18.FechaFin, Horas = 5, FechaActividad = act18.FechaInicio, FechaRegistro = act18.FechaRegistro, Descripcion = "Armado y desarmado de stands" }
            };
            contexto.HorasVoluntariado.AddRange(horasList);
            await contexto.SaveChangesAsync();

            // === CERTIFICADOS ===
            var certList = new List<Certificado>
            {
                new() {
                    VoluntarioId = vol1.Id, ActividadId = act16.Id, OrganizacionId = org2.Id,
                    NumeroSerie = $"CERT-{hoy:yyyyMMdd}-A1B2C3D4",
                    FechaEmision = hoy.AddDays(-1), FechaVencimiento = hoy.AddYears(2).AddDays(-1),
                    HorasTotales = 8, TemaEspecifico = "Limpieza y conservaci\u00f3n ambiental",
                    FirmanteNombre = "Carlos Mendoza", FirmanteCargo = "Director Ejecutivo - EcoVida Bolivia",
                    Estado = EstadoCertificado.Generado
                },
                new() {
                    VoluntarioId = vol3.Id, ActividadId = act16.Id, OrganizacionId = org2.Id,
                    NumeroSerie = $"CERT-{hoy:yyyyMMdd}-E5F6G7H8",
                    FechaEmision = hoy.AddDays(-1), FechaVencimiento = hoy.AddYears(2).AddDays(-1),
                    HorasTotales = 8, TemaEspecifico = "Coordinaci\u00f3n de jornada ambiental",
                    FirmanteNombre = "Carlos Mendoza", FirmanteCargo = "Director Ejecutivo - EcoVida Bolivia",
                    Estado = EstadoCertificado.Generado
                },
                new() {
                    VoluntarioId = vol3.Id, ActividadId = act17.Id, OrganizacionId = org1.Id,
                    NumeroSerie = $"CERT-{hoy:yyyyMMdd}-I9J0K1L2",
                    FechaEmision = hoy.AddDays(-1), FechaVencimiento = hoy.AddYears(2).AddDays(-1),
                    HorasTotales = 10, TemaEspecifico = "Organizaci\u00f3n de jornada m\u00e9dica gratuita",
                    FirmanteNombre = "Mar\u00eda L\u00f3pez", FirmanteCargo = "Directora - Fundaci\u00f3n Esperanza",
                    Estado = EstadoCertificado.Generado
                },
                new() {
                    VoluntarioId = vol4.Id, ActividadId = act18.Id, OrganizacionId = org3.Id,
                    NumeroSerie = $"CERT-{hoy:yyyyMMdd}-M3N4O5P6",
                    FechaEmision = hoy.AddDays(-1), FechaVencimiento = hoy.AddYears(2).AddDays(-1),
                    HorasTotales = 5, TemaEspecifico = "Atenci\u00f3n al p\u00fablico en evento solidario",
                    FirmanteNombre = "Ana R\u00edos", FirmanteCargo = "Coordinadora - Manos Solidarias",
                    Estado = EstadoCertificado.Generado
                }
            };
            contexto.Certificados.AddRange(certList);
            await contexto.SaveChangesAsync();

            // Usuarios para pruebas (contraseña: 123456)
            var adminUser = new Usuario
            {
                Nombre = "Admin",
                Apellido = "Sistema",
                Email = "admin@voluntariado.com",
                PasswordHash = PasswordHash,
                Rol = RolUsuario.Admin,
                FechaRegistro = fechaBase.AddMonths(-6),
                Activo = true
            };

            var orgUser = new Usuario
            {
                Nombre = "María",
                Apellido = "López",
                Email = "org@voluntariado.com",
                PasswordHash = PasswordHash,
                Rol = RolUsuario.Organizacion,
                OrganizacionId = org1.Id,
                FechaRegistro = fechaBase.AddMonths(-6),
                Activo = true
            };

            var volUser = new Usuario
            {
                Nombre = "Pedro",
                Apellido = "García",
                Email = "vol@voluntariado.com",
                PasswordHash = PasswordHash,
                Rol = RolUsuario.Voluntario,
                VoluntarioId = vol1.Id,
                FechaRegistro = fechaBase.AddMonths(-5),
                Activo = true
            };

            contexto.Usuarios.AddRange(adminUser, orgUser, volUser);
            await contexto.SaveChangesAsync();
        }
    }
}
