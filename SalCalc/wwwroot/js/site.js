class User {


    GetUsers() {
        $.get('/Home/GetUser', {}, function (data) {
            $("#UserTable").html(data);
        });
    }

    OpenAddUser()
    {
        let user_table = $("#UserTable");
        let User_Add = $("#UserAdd");
        if(user_table.css("display")==='block')
        {
            $("#AddOrTableBut").val("Назад");
            user_table.css({ 'display' : 'none'});
            User_Add.css({ 'display' : 'block'});
        }
        else {
            $("#AddOrTableBut").val("Добавить");
            user_table.css({ 'display' : 'block'});
            User_Add.css({ 'display' : 'none'});
        }
            
    }
    DeleteUser(id, fio)
    {
        let result = confirm("Вы действительно хотите удалить пользователя "+fio+"?");
        if(!result)
            return;
        let update_user= this.GetUsers;
        $.post('/Home/DeleteUser', { id:id }, function (data) {
            alert(data);
            update_user();
        });
    }
    AddUser()
    {
        let update_user= this.GetUsers;
        $.get('/Home/AddUser', { Id:0, FirstName:$('#FirstName').val(), LastName:$('#LastName').val(),SecondName:$('#SecondName').val(), Position:$('#Position').val(), PhoneNumber:$('#PhoneNumber').val(), Email:$('#Email').val()  }, function (data) {
        alert(data);
        
        if(data==="Пользователь успешно добавлен")
        {
            $('#FirstName').val('')
            $('#SecondName').val('')
            $('#LastName').val('')
            $('#Position').val('')
            $('#PhoneNumber').val('')
            $('#Email').val('')
        }
            update_user();
        })
        
    }
}
const  user=new User();

class Salary
{
    OpenAddSalary()
    {
        let user_table = $("#SalaryTable");
        let User_Add = $("#SalaryAdd");
        if(user_table.css("display")==='block')
        {
            $("#AddOrTableBut").val("Назад");
            user_table.css({ 'display' : 'none'});
            User_Add.css({ 'display' : 'block'});
        }
        else {
            $("#AddOrTableBut").val("Добавить");
            user_table.css({ 'display' : 'block'});
            User_Add.css({ 'display' : 'none'});
        }

    }
    GetSalary() {
        $.get('/Salary/GetSalary', {}, function (data) {
            $("#SalaryTable").html(data);
        });
    }
    GetUserPosition()
    {
        let id=$("#SelectEmp").val();
        $.ajax({
            type: 'GET',
            url: '/Home/GetEmployee',
            data: {id:id},
            dataType:"json",
            success: function (result) {
               $("#Position").val(result["position"]);
            }
        });
    }
    GetUserPositionTable(Id, input)
    {
        let id=$("#"+Id).val();
        $.ajax({
            type: 'GET',
            url: '/Home/GetEmployee',
            data: {id:id},
            dataType:"json",
            success: function (result) {
                $("#"+input).val(result["position"]);
            }
        });
    }
    AddSalary()
    {
        let get_sal= this.GetSalary;
        $.get('/Salary/AddSalary', { Id:0, SalaryReceived:$('#Salary').val(), MonthId :$('#MonthSelect').val(),EmployeeId:$('#SelectEmp').val(), TypeSalaryId:$('#TypeSalarySelect').val(), SystemSalaryId:$('#SystemSalarySelect').val(), Year:$('#YearSelect').val()  }, function (data) {
            alert(data);
            if(data==="Заплата успешно добавлена")
            {
                $('#Salary').val('')
                $('#Position').val('');
                $('#SelectEmp').val(0)
                $('#SystemSalarySelect').val(0)
                $('#YearSelect').val(0)
                $('#TypeSalarySelect').val(0)
                $('#MonthSelect').val(0)
            }
            get_sal();
        });
    }
    DeleteSalary(id, fio)
    {
        let result = confirm("Вы действительно хотите удалить зарплату пользователя "+fio+"?");
        if(!result)
            return;
        let getSalary= this.GetSalary;
        $.post('/Salary/DeleteSalary', { id:id }, function (data) {
            alert(data);
            getSalary();
        });
    }
}
const salary=new Salary();

class Report
{
    GetReport() {
        $.get('/Report/GetReport', {}, function (data) {
            $("#ReportList").html(data);
        });
    }s
    OpenMonth()
    {
        if($("#check_mouth").is(":checked"))
        {
            $("#Month").css({ 'visibility' : 'visible'});
        }
        else
        {
            let month=$("#Month");
            month.css({ 'visibility' : 'collapse'});
            month.val('');
        }
    }
    AddUserInReport()
    {
        let get_report=this.GetReport;
        $.post('/Report/AddUserInReport', { id:$("#SelectEmp").val(), year:$("#Year").val(), month:$("#Month").val(), withMonth:$("#check_mouth").is(":checked")  }, function (data) {
            get_report();
        });
    }
}
const  report=new Report();