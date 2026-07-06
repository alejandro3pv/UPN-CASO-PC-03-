using System;

public class CalculadoraSLA
{
    static readonly TimeSpan HoraInicio = new TimeSpan(9, 0, 0);
    static readonly TimeSpan HoraFin    = new TimeSpan(17, 0, 0);
    const double LimiteSLA = 8.0;

    public static double CalcularHorasLaborales(DateTime fechaCreacion, DateTime fechaResolucion)
    {
        double horasLaborales = 0;
        DateTime fechaActual = fechaCreacion.Date;

        while (fechaActual <= fechaResolucion.Date)
        {
            if (fechaActual.DayOfWeek != DayOfWeek.Saturday &&
                fechaActual.DayOfWeek != DayOfWeek.Sunday)
            {
                DateTime inicioJornada = fechaActual + HoraInicio;
                DateTime finJornada    = fechaActual + HoraFin;

                if (fechaActual == fechaCreacion.Date && fechaCreacion > inicioJornada)
                    inicioJornada = fechaCreacion;

                if (fechaActual == fechaResolucion.Date && fechaResolucion < finJornada)
                    finJornada = fechaResolucion;

                if (finJornada > inicioJornada)
                    horasLaborales += (finJornada - inicioJornada).TotalHours;
            }

            fechaActual = fechaActual.AddDays(1);
        }

        return horasLaborales;
    }

    public static string EvaluarSLA(DateTime fechaCreacion, DateTime fechaResolucion)
    {
        double horas = CalcularHorasLaborales(fechaCreacion, fechaResolucion);

        if (horas < LimiteSLA)
            return "Cumplido";
        else
            return $"Incumplido: {horas - LimiteSLA} horas de mas";
    }

    public static void Main()
    {
        DateTime creacion   = new DateTime(2023, 10, 10, 14, 0, 0);
        DateTime resolucion = new DateTime(2023, 10, 11, 10, 0, 0);

        Console.WriteLine($"Horas laborales: {CalcularHorasLaborales(creacion, resolucion)}");
        Console.WriteLine(EvaluarSLA(creacion, resolucion));
    }
}
