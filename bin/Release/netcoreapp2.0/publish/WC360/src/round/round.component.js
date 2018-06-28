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
var RoundComponent = /** @class */ (function () {
    function RoundComponent(http) {
        this.http = http;
        this.fixtures = [];
        this.dates = [];
        this.fixtureDates = [];
    }
    // tslint:disable-next-line:use-life-cycle-interface
    RoundComponent.prototype.ngOnChanges = function () {
        var _this = this;
        if (!this.round) {
            return;
        }
        this.http.get('http://localhost:2323/api/fixture/round/' + this.round.id).subscribe(function (data) { return _this.initFixtures(data); });
    };
    RoundComponent.prototype.initFixtures = function (fixtures) {
        var _this = this;
        fixtures.forEach(function (fixture) {
            var d = fixture.date;
            fixture.date = _this.toDate(fixture.date);
            var fd = _this.fixtureDates.find(function (fixtureDate) {
                return fixtureDate.date.getTime() === _this.toDayDate(d).getTime();
            });
            if (fd == null) {
                fd = new FixtureDate(_this.toDayDate(d));
                _this.fixtureDates.push(fd);
            }
            fd.fixtures.push(fixture);
        });
    };
    RoundComponent.prototype.getFixturesForDate = function (date) {
        return this.fixtures.filter(function (fixture) { return fixture.date === date; });
    };
    RoundComponent.prototype.dateWithoutTime = function (date) {
        return new Date(date.getDate() + '-' + date.getMonth() + '-' + date.getFullYear());
    };
    RoundComponent.prototype.toDate = function (date) {
        var dateParts = date.split('-');
        var timeParts = dateParts[2].split(':');
        return new Date(+dateParts[0], +dateParts[1] - 1, +(dateParts[2].substr(0, 2)), timeParts[0].substr(3, 5), timeParts[1]);
    };
    RoundComponent.prototype.toDayDate = function (date) {
        var dateParts = date.split('-');
        return new Date(+dateParts[0], +dateParts[1] - 1, +(dateParts[2].substr(0, 2)));
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], RoundComponent.prototype, "round", void 0);
    RoundComponent = __decorate([
        core_1.Component({
            // tslint:disable-next-line:component-selector
            selector: 'round',
            templateUrl: './round.component.html',
            styleUrls: ['./round.component.css']
        }),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], RoundComponent);
    return RoundComponent;
}());
exports.RoundComponent = RoundComponent;
var FixtureDate = /** @class */ (function () {
    function FixtureDate(date) {
        this.fixtures = [];
        this.date = date;
    }
    return FixtureDate;
}());
//# sourceMappingURL=round.component.js.map