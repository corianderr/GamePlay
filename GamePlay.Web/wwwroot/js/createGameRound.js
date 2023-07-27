let minPlayerCount, maxPlayerCount, currentPlayerCount;

const createGameRound = {
    initialize: function (min, max, currentNumber) {
        currentPlayerCount = currentNumber;
        minPlayerCount = min;
        maxPlayerCount = max;
    },
};
    
document.addEventListener('DOMContentLoaded', function() {
    let now = new Date(),
        maxDate = now.toISOString().substring(0,10);
    $('#GameRound_Date').prop('max', maxDate);
});

$(document).on('click', '#show-new-player', function () {
    if (currentPlayerCount >= maxPlayerCount) {
        alert(`You can't add more than ${maxPlayerCount} players.`);
        return;
    }
    toggleNewPlayer();
});

$("#player-is-registered").change(function() {
    if (this.checked) {
        $("#user-select-div").show();
    } else {
        $('#user-select-div select option[value="none"]').attr("selected", true);
        $("#user-select-div").hide();
    }
});

function toggleNewPlayer() {
    const newPlayer = $('#newPlayer');
    
    if (!newPlayer.is(':visible')) {
        clearNewPlayer();
        newPlayer.show();
    } else {
        newPlayer.hide();
    }
}

function clearNewPlayer() {
    $('#player-name').val('');
    $('#player-role').val('');
    $('#player-score').val('');
    $('#player-is-winner').prop('checked', false);
    $('#player-is-registered').prop('checked', false);
    $('#user-select-div select option[value="none"]').attr("selected", true);
    $("#user-select-div").hide();
}

$(document).on('click', '#add-player-button', function () {
    addPlayer();
});

function addPlayer() {
    const name = $('#player-name').val().trim();
    const role = $('#player-role').val().trim();
    const score = $('#player-score').val().trim();
    const userId = $( "#player-user-id" ).val();
    console.log(userId);
    const isRegistered = $('#player-is-registered').prop('checked');

    if (name !== '' && role !== '' && score !== '' && (!isRegistered || userId !== 'none')) {
        const isWinner = $('#player-is-winner').prop('checked');

        const rowHtml = `<tr><td>${name}</td><td>${role}</td><td>${score}</td>
            <td><input type="checkbox" disabled ${isWinner ? 'checked' : ''}/></td>
            <td><input type="checkbox" disabled ${isRegistered ? 'checked' : ''}/></td>
            <td>${isRegistered ? userId : ''}</td>
            <td><input type="button" value="✖" class="btn btn-danger float-right delete-button"/></td>
            </tr>`;
        $('#player-table tbody').append(rowHtml);
        currentPlayerCount++;
        toggleNewPlayer()
    }
    validatePlayer();
}

function validatePlayer(){
    validate('#player-name');
    validate('#player-role');
    validate('#player-score');
    validate('#player-user-id');
}

function validate(id){
    const playerNameInput = $(id);
    (playerNameInput.val() === '' || playerNameInput.val() === 'none') ? playerNameInput.addClass('error') : playerNameInput.removeClass('error');
}

$(document).on('click', '.delete-button', function () {
    $(this).closest('tr').remove();
    currentPlayerCount--;
});

function collectPlayers(){
    let players = [];
    $('#player-table tbody tr').each(function () {
        let player = {
            Name: $(this).find('td').eq(0).text(),
            Role: $(this).find('td').eq(1).text(),
            Score: $(this).find('td').eq(2).text(),
            IsWinner: $(this).find('td').eq(3).find('input').is(":checked"),
            IsRegistered: $(this).find('td').eq(4).find('input').is(":checked"),
            UserId: $(this).find('td').eq(5).text()
        };
        players.push(player);
    });
    return players;
}

function collectEditPlayers(){
    let players = [];
    $('#player-table tbody tr').each(function () {
        console.log($(this).find('td').eq(6).text());
        let player = {
            Name: $(this).find('td').eq(0).text(),
            Role: $(this).find('td').eq(1).text(),
            Score: $(this).find('td').eq(2).text(),
            IsWinner: $(this).find('td').eq(3).find('input').is(":checked"),
            IsRegistered: $(this).find('td').eq(4).find('input').is(":checked"),
            UserId: $(this).find('td').eq(5).text(),
            Id: ($(this).find('td').eq(6).text() !== '' ? $(this).find('td').eq(6).text() : '00000000-0000-0000-0000-000000000000')
        };
        players.push(player);
    });
    return players;
}

$('#gameForm').submit(function (e) {
    console.log('submit');
    e.preventDefault();
    
    if (currentPlayerCount < minPlayerCount) {
        alert('Minimum number of players not met. Please add more players.');
        return;
    }
    
    const players = collectPlayers();
    const data = {
        'GameRound': {
            'GameId': $('#GameRound_GameId').val(),
            'Date': $('#GameRound_Date').val(),
            'Place': $('#GameRound_Place').val(),
            'Players': players
        }
    };
    console.log(data)

    const form = $('#__AjaxAntiForgeryForm');
    const token = $('input[name="__RequestVerificationToken"]', form).val();
    $.post("Create", 
        {
            __RequestVerificationToken: token, 
            createViewModel: data
        })
        .done(function (response) {
            console.log(response);
            if (response.success) {
                console.log("OK");
                window.location.href = response.redirectToUrl;
            }
        });
});

$('#game-edit-form').submit(function (e) {
    console.log('submit');
    e.preventDefault();

    if (currentPlayerCount < minPlayerCount) {
        alert('Minimum number of players not met. Please add more players.');
        return;
    }

    const players = collectEditPlayers();
    const roundId = $('#GameRound_Id').val();
    const data = {
        'GameRound': {
            'Id': roundId,
            'GameId': $('#GameRound_GameId').val(),
            'Date': $('#GameRound_Date').val(),
            'Place': $('#GameRound_Place').val(),
            'Players': players
        }
    };
    console.log(data)

    const form = $('#__AjaxAntiForgeryForm');
    const token = $('input[name="__RequestVerificationToken"]', form).val();
    $.post("Edit",
        {
            __RequestVerificationToken: token,
            id: roundId,
            updateViewModel: data
        })
        .done(function (response) {
            console.log(response);
            if (response.success) {
                console.log("OK");
                window.location.href = response.redirectToUrl;
            }
        });
});