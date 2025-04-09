function deleteTodo(id) {
    if (confirm("Are you sure you want to delete this todo item?")) {
        // Call your backend to delete the TodoItem
        window.location.href = '/TodoItems/Delete/' + id; // This should be your delete route
    }
}

function resetFormForCreate() {
    $("#Todo_Name").val('');
    $("#Todo_Id").val(''); // In case you have a hidden ID input
    $("#form-button").val("Create Todo");
    $("#form-action").attr("action", "/TodoItems/Create");
}

function populateForm(id) {
    $.ajax({
        url: '/TodoItems/Edit', // Adjust the URL to match your action
        type: 'GET',
        data: { id: id },
        success: function (response) {
            if (response) {
                // Populate the form fields with the response
                $("#Todo_Name").val(response.name);
                $("#Todo_Id").val(response.id);
                $("#form-button").val("Update Todo");
                $("#form-action").attr("action", "/TodoItems/Edit"); // Adjust action URL for update
            } else {
                alert('Todo item not found :3');
            }
        },
        error: function () {
            alert('Failed to load Todo item :3');
        }
    });
}

    


function ToggleChange(id) {
    // Update the 'IsEnded' status via an AJAX request
    $.ajax({
        url: '/TodoItems/ToggleChange', // Adjust the URL
        type: 'POST',
        data: { id: id },
        success: function () {
            console.log('Status updated successfully');
        },
        error: function () {
            alert('Failed to update the status');
        }
    });
}