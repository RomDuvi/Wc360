"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var auth_service_1 = require("../../authservice/auth.service");
var router_1 = require("@angular/router");
var Bet_1 = require("../../models/Bet");
var RoundTeamComponent = /** @class */ (function () {
    function RoundTeamComponent(http, auth, router) {
        this.http = http;
        this.auth = auth;
        this.router = router;
        this.rounds = [];
        this.teams = [];
        this.selectedTeam = null;
        this.selectedRound = null;
    }
    // tslint:disable-next-line:use-life-cycle-interface
    RoundTeamComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.auth.connectedUser().then(function (data) {
            _this.http.get('http://localhost:2323/api/team')
                .toPromise().then(function (teams) {
                _this.teams = teams.map(function (x) {
                    return { id: x.id, name: x.name };
                });
                _this.http.get('http://localhost:2323/api/round')
                    .toPromise().then(function (rounds) {
                    _this.rounds = rounds.filter(function (y) { return y.id !== 1; }).map(function (x) {
                        return { id: x.id, name: x.name };
                    });
                    _this.http.get('http://localhost:2323/api/bet/roundTeam/player/' + data.userName)
                        .subscribe(function (bet) {
                        if (!bet) {
                            return;
                        }
                        var roundName = rounds.find(function (r) { return r.id === bet.roundTeamRoundId; }).name;
                        var teamName = teams.find(function (t) { return t.id === bet.roundTeamId; }).name;
                        _this.bet = { round: roundName, team: teamName };
                    });
                });
            });
        });
    };
    RoundTeamComponent.prototype.saveBet = function () {
        var _this = this;
        this.auth.connectedUser().then(function (data) {
            _this.http.post('http://localhost:2323/api/bet/', new Bet_1.Bet(null, null, false, data.id, null, null, null, new Date(), false, _this.selectedTeam, _this.selectedRound), { headers: { 'Content-Type': 'application/json; charset=utf-8' } })
                .toPromise()
                .then(function (x) {
                _this.router.navigateByUrl('/bet');
            });
        });
    };
    RoundTeamComponent = __decorate([
        core_1.Component({
            // tslint:disable-next-line:component-selector
            selector: 'round-team',
            templateUrl: './roundteam.component.html',
            styleUrls: ['./roundteam.component.css']
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, auth_service_1.AuthService, router_1.Router])
    ], RoundTeamComponent);
    return RoundTeamComponent;
}());
exports.RoundTeamComponent = RoundTeamComponent;
//# sourceMappingURL=roundteam.component.js.map