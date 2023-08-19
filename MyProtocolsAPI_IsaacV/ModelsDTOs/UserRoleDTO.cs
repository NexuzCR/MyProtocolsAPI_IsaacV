namespace MyProtocolsAPI_IsaacV.ModelsDTOs
{
    public class UserRoleDTO
    {
        //un DTO(data transfer object) sirve para dos funciones:
        //1- simplificar la estructura de los json que se envian y llegan a los endpoint de lors controllers
        //quitando composiciones innecesarias que solo haria que las json sean muy pesados o que muestren inmformacion
        //que no se deasea ver(PUEDE SER POR SEGURIDAD)
        //2- ocultar la extructura real de los modelos y por tanto de las tablas de de bases de datos, alo que los programadores
        // de las apps, paginas web o aplicaciones de escritorio.

        //tomando en cuienta el segundo criterio y solo a manera de ejemplo este DTO tendra los nombres de propiedades
        //en español

        public int IDRolUsuario { get; set; }
        public string DescripcionRol { get; set; } = null!;

    }
}
