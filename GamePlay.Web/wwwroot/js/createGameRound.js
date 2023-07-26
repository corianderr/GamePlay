
let minPlayerCount, maxPlayerCount, currentPlayerCount;

let createGameRound = {
    initialize: function (min, max) {
        console.log(min);
        console.log(max);
        currentPlayerCount = 0;
        minPlayerCount = min;
        maxPlayerCount = max;
    },
};
    
document.addEventListener('DOMContentLoaded', function() {
    let now = new Date(),
        maxDate = now.toISOString().substring(0,10);
    $('#my-date-input').prop('max', maxDate);
});

$(document).on('click', '#show-new-player', function () {
    if (currentPlayerCount >= maxPlayerCount) {
        alert(`You can't add more than ${maxPlayerCount} players.`);
        return;
    }
    toggleNewPlayer();
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
}

$(document).on('click', '#add-player-button', function () {
    addPlayer();
});

function addPlayer() {
    console.log(currentPlayerCount)
    
    removeValidation();
    const name = $('#player-name').val().trim();
    const role = $('#player-role').val().trim();
    const score = $('#player-score').val().trim();

    if (name !== '' && role !== '' && score !== '') {
        const isWinner = $('#player-is-winner').prop('checked');
        const isRegistered = $('#player-is-registered').prop('checked');

        const rowHtml = `<tr><td>${name}</td><td>${role}</td><td>${score}</td>
            <td><input type="checkbox" disabled ${isWinner ? 'checked' : ''}/></td>
            <td><input type="checkbox" disabled ${isRegistered ? 'checked' : ''}/></td>
            <td><input type="button" value="✖" class="btn btn-danger float-right delete-button"/></td>
            </tr>`;
        $('#player-table tbody').append(rowHtml);
        currentPlayerCount++;
        toggleNewPlayer()
    } else {
        if (name === '') {
            $('#player-name').addClass('error');
        }
        if (role === '') {
            $('#player-role').addClass('error');
        }
        if (score === '') {
            $('#player-score').addClass('error');
        }
    }
}

function removeValidation(){
    $('#player-name').removeClass('error');
    $('#player-role').removeClass('error');
    $('#player-score').removeClass('error');
}

$(document).on('click', '.delete-button', function () {
    $(this).closest('tr').remove();
    currentPlayerCount--;
});

$('#gameForm').submit(function (e) {
    console.log('submit');
    e.preventDefault();
    
    if (currentPlayerCount < minPlayerCount) {
        alert('Minimum number of players not met. Please add more players.');
        return;
    }
    
    let players = collectPlayers();
});

function collectPlayers(){
    let players = [];
    $('#player-table tbody tr').each(function () {
        let player = {
            Name: $(this).find('td').eq(0).text(),
            Role: $(this).find('td').eq(1).text(),
            Score: $(this).find('td').eq(2).text(),
            IsWinner: $(this).find('td').eq(3).find('input').is(":checked"),
            IsRegistered: $(this).find('td').eq(4).find('input').is(":checked")
        };
        players.push(player);
    });
    return players;
}