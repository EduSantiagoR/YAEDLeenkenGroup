$(document).ready(function () {
    GetAll();
})

function GetAll() {
    $.ajax({
        type: 'GET',
        url:'http://localhost:64673/api/empleado',
        success: function (result) {
            $('#tablaEmpleados tbody').empty();
            $.each(result.Objects, function (i, Empleado) {
                var filas = '<tr>' +
                    '<td>'
                    +'<a href="#" class="btn btn-success">'+'</a>'
                    + '</td>'
                    + '<td>' + Empleado.NumeroNomina +'</td>'
                    + '<td>' + Empleado.Nombre +'</td>'
                    + '<td>' + Empleado.ApellidoPaterno +'</td>'
                    + '<td>' + Empleado.ApellidoMaterno + '</td>'
                    + '<td>' + Empleado.Estado.Nombre + '</td>'
                    + '<td>' + '<button onclick="Delete(' + Empleado.Id + ')" class="btn btn-danger"></botton>' + '</td>'
                    +'</tr>';
                $('#tablaEmpleados tbody').append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
}
function Delete(idEmpleado) {
    if (confirm('¿Estas seguro de querer eliminar?')) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:64673/api/empleado/' + parseInt(idEmpleado),
            success: function (result) {
                $('#Modal').modal();
                GetAll();
            },
            error: function (result) {
                alert('Error al eliminar' + result.responseJSON.ErrorMessage);
            }
        });
    }
}