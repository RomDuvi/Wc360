"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Bet = /** @class */ (function () {
    // tslint:disable-next-line:max-line-length
    function Bet($id, $fixtureId, $isDraw, $playerId, $scoreTeam1, $scoreTeam2, $winnerTeamId, $date, $checked, $roundTeamId, $roundTeamRoundId) {
        this.id = $id;
        this.fixtureId = $fixtureId;
        this.isDraw = $isDraw;
        this.playerId = $playerId;
        this.scoreTeam1 = $scoreTeam1;
        this.scoreTeam2 = $scoreTeam2;
        this.winnerTeamId = $winnerTeamId;
        this.date = $date;
        this.roundTeamId = $roundTeamId;
        this.checked = $checked;
        this.roundTeamRoundId = $roundTeamRoundId;
    }
    return Bet;
}());
exports.Bet = Bet;
//# sourceMappingURL=Bet.js.map