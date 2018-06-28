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
var QualifTeamComponent = /** @class */ (function () {
    function QualifTeamComponent(http, auth, router) {
        this.http = http;
        this.auth = auth;
        this.router = router;
        this.qualifTeams = [];
        this.bet = false;
        this.selectedNumber = 16;
    }
    // tslint:disable-next-line:use-life-cycle-interface
    QualifTeamComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.auth.connectedUser().then(function (data) {
            _this.http.get('http://localhost:2323/api/team')
                .toPromise()
                .then(function (x) {
                x.forEach(function (team) {
                    var t = new QualifTeam(false, team);
                    _this.qualifTeams.push(t);
                });
                _this.http.get('http://localhost:2323/api/bet/qualifteam/player/' + data.id)
                    .toPromise()
                    .then(function (y) {
                    _this.bet = y.length > 0;
                    _this.selectedNumber = 0;
                    y.forEach(function (id) {
                        _this.qualifTeams.find(function (qt) { return qt.team.id === id; }).selected = true;
                    });
                });
            });
        });
    };
    QualifTeamComponent.prototype.checkChange = function () {
        var _this = this;
        this.selectedNumber = 16;
        this.qualifTeams.forEach(function (team) {
            if (team.selected) {
                _this.selectedNumber--;
            }
        });
    };
    QualifTeamComponent.prototype.saveBet = function () {
        var _this = this;
        this.auth.connectedUser().then(function (data) {
            _this.http.post('http://localhost:2323/api/bet/qualifteam/' + data.id, _this.qualifTeams.filter(function (x) { return x.selected; }).map(function (x) { return ({ playerId: data.id, teamId: x.team.id }); }), { headers: { 'Content-Type': 'application/json; charset=utf-8' } })
                .subscribe(function (x) {
                _this.router.navigateByUrl('/bet');
            });
        });
    };
    QualifTeamComponent = __decorate([
        core_1.Component({
            // tslint:disable-next-line:component-selector
            selector: 'qualif-team',
            templateUrl: './qualifteam.component.html',
            styleUrls: ['./qualifteam.component.css']
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, auth_service_1.AuthService, router_1.Router])
    ], QualifTeamComponent);
    return QualifTeamComponent;
}());
exports.QualifTeamComponent = QualifTeamComponent;
var QualifTeam = /** @class */ (function () {
    function QualifTeam(selected, team) {
        this.selected = selected;
        this.team = team;
    }
    return QualifTeam;
}());
//# sourceMappingURL=qualifteam.component.js.map