$(document).ready(function () {
    GetAll();
})

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64673/api/empleado',
        success: function (result) {
            $('#tablaEmpleados tbody').empty();
            $.each(result.Objects, function (i, Empleado) {
                var filas = '<tr>' +
                    '<td>'
                    + '<button onclick="GetById(' + Empleado.Id + ')" class="btn btn-warning"></botton>' + '</a>'
                    + '</td>'
                    + '<td>' + Empleado.NumeroNomina + '</td>'
                    + '<td>' + Empleado.Nombre + '</td>'
                    + '<td>' + Empleado.ApellidoPaterno + '</td>'
                    + '<td>' + Empleado.ApellidoMaterno + '</td>'
                    + '<td>' + Empleado.Estado.Nombre + '</td>'
                    + '<td>' + '<button onclick="Delete(' + Empleado.Id + ')" class="btn btn-danger"></botton>' + '</td>'
                    + '</tr>';
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
            type: 'DELETE',
            url: 'http://localhost:64673/api/empleado/' + parseInt(idEmpleado),
            success: function (result) {
                $('#Modal').modal();
                GetAll();
            },
            error: function (result) {
                alert('Error al eliminar' + result.ErrorMessage);
            }
        });
    }
}



function Guardar() {
    var id = parseInt($('#txtIdEmpleado').val());
    if (!isNaN(id))  {
        UpdateEmpleado(id);
    }else{
        AddEmpleado();
    }
}

function Limpiar() {

    $('#txtIdEmpleado').val('');
    $('#txtNumeroNomina').val(''),
    $('#txtNombre').val(''),
    $('#txtApellidoPaterno').val(''),
    $('#txtApellidoMaterno').val(''),
    $('#txtEstado').val('')
    


}
function AddEmpleado() {
    var nuevoEmpleado = {
        
            Id: 0,
            NumeroNomina: $('#txtNumeroNomina').val(),
            Nombre: $('#txtNombre').val(),
            ApellidoPaterno: $('#txtApellidoPaterno').val(),
            ApellidoMaterno: $('#txtApellidoMaterno').val(),
            Estado: {
                IdEstado: $('#txtEstado').val()
            }
        };

        $.ajax({
            type: 'POST',
            url: 'http://localhost:64673/api/empleado',
            dataType: 'json',
            data: nuevoEmpleado,
            success: function (result) {
                $('#myModal').modal('show');
                Limpiar();
                GetAll();
            },
            error: function (result) {
                alert('Error al agregar empleado: ' + result.ErrorMessage);
            }
        });

    modalClose();
    }
   


function GetById(id) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:64673/api/empleado/' + parseInt(id),
        success: function (result) {
            $.each(result.Objects, function (i, Empleado) {
                $('#txtIdEmpleado').val(Empleado.Id);
                $('#txtNumeroNomina').val(Empleado.NumeroNomina);
                $('#txtNombre').val(Empleado.Nombre);
                $('#txtApellidoPaterno').val(Empleado.ApellidoPaterno);
                $('#txtApellidoMaterno').val(Empleado.ApellidoMaterno);
                $('#txtEstado').val(Empleado.Estado.IdEstado);
            });
            $('#myModal').modal('show');

        },
        error: function (result) {
            alert('Error al obtener el empleado: ' + result.ErrorMessage);
        }

    });

};


function UpdateEmpleado(idEmpleado) {

    var NuevoEmpleado = {
        Id: 0,
        NumeroNomina: $('#txtNumeroNomina').val(),
        Nombre: $('#txtNombre').val(),
        ApellidoPaterno: $('#txtApellidoPaterno').val(),
        ApellidoMaterno: $('#txtApellidoMaterno').val(),
        Estado: {
            IdEstado: $('#txtEstado').val()
        }
    };

    $.ajax({
        type: 'PUT',
        url: 'http://localhost:64673/api/empleado/' + parseInt(idEmpleado),
        datatype: 'JSON',
        data: NuevoEmpleado,
        success: function (result) {
            $('#myModal').modal('show');
            GetAll();
        
        },
        error: function (result) {
            alert('Error en la consulta.' + result.ErrorMessage);
        }
    });
    modalClose();
};

function modalClose() {
    $('#myModal').modal('hide');
}