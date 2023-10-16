﻿$(document).ready(function () {
    GetAll();
    EstadoGetAll();
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
            alert('Error en la consulta.' + result.ErrorMessage);
        }
    });
}
function Delete(idEmpleado) {
    if (confirm('¿Estas seguro de querer eliminar?')) {
        $.ajax({
            url: 'http://localhost:64673/api/empleado/' + idEmpleado,
            type: 'POST',
            success: function (result) {
                GetAll();
            },
            error: function (result) {
                alert('Error al eliminar ' + result.ErrorMessage);
            }
        });
    }
}
function EstadoGetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64673/api/estado',
        success: function (result) {
            $('#ddlEstados').append('<option value="' + 0 + '">' + 'Selecciona un estado' + '</option>');
            $.each(result.Objects, function (i, Estado) {
                $('#ddlEstados').append('<option value="' + Estado.IdEstado + '">' + Estado.Nombre + '</option>');
            });
        },
        error: function (result) {
            alert('Error al consultar estados', result.ErrorMessage);
        }
    });
}