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
var Bet_1 = require("../../models/Bet");
var router_1 = require("@angular/router");
var WinnerTeamComponent = /** @class */ (function () {
    function WinnerTeamComponent(http, auth, router) {
        this.http = http;
        this.auth = auth;
        this.router = router;
        this.teams = [];
        this.selected = null;
    }
    // tslint:disable-next-line:use-life-cycle-interface
    WinnerTeamComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.http.get('http://localhost:2323/api/team').toPromise()
            .then(function (x) {
            _this.teams = x.filter(function (xt) { return xt.eleminated === false; }).map(function (t) {
                return { id: t.id, name: t.name };
            });
            _this.auth.connectedUser().then(function (a) {
                _this.http.get('http://localhost:2323/api/bet/winnerTeam/player/' + a.userName)
                    .subscribe(function (w) {
                    _this.winningTeam = x.find(function (elem) { return elem.id === w.winnerTeamId; });
                    console.log(_this.winningTeam.eleminated + '-' + _this.isInBetDate());
                    _this.canBet = _this.winningTeam.eleminated && _this.isInBetDate();
                    console.log(_this.canBet);
                });
            });
        });
    };
    WinnerTeamComponent.prototype.saveBet = function () {
        var _this = this;
        this.auth.connectedUser().then(function (data) {
            _this.http.post('http://localhost:2323/api/bet/', new Bet_1.Bet(null, null, false, data.id, null, null, _this.selected, new Date(), false, null, null), { headers: { 'Content-Type': 'application/json; charset=utf-8' } })
                .subscribe(function (x) {
                _this.router.navigateByUrl('/bet');
            });
        });
    };
    WinnerTeamComponent.prototype.isInBetDate = function () {
        var betDates = [new BetDate(new Date(2018, 5, 7, 23, 59, 59, 0), new Date(2018, 5, 14, 13, 0, 0, 0)),
            new BetDate(new Date(2018, 5, 29, 0, 0, 0, 0), new Date(2018, 5, 30, 15, 0, 0)),
            new BetDate(new Date(2018, 6, 4, 0, 0, 0, 0), new Date(2018, 6, 6, 0, 0, 0, 0))];
        var currentDate = new Date();
        for (var i = 0; i < betDates.length; i++) {
            var date = betDates[i];
            if (currentDate > date.startDate && currentDate < date.endDate) {
                return true;
            }
        }
        return false;
    };
    WinnerTeamComponent = __decorate([
        core_1.Component({
            // tslint:disable-next-line:component-selector
            selector: 'winner-team',
            templateUrl: './winnerteam.component.html',
            styleUrls: ['./winnerteam.component.css']
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, auth_service_1.AuthService, router_1.Router])
    ], WinnerTeamComponent);
    return WinnerTeamComponent;
}());
exports.WinnerTeamComponent = WinnerTeamComponent;
var BetDate = /** @class */ (function () {
    function BetDate(startDate, endDate) {
        this.startDate = startDate;
        this.endDate = endDate;
    }
    return BetDate;
}());
//# sourceMappingURL=winnerteam.component.js.map