"use strict";
var Modules;
(function (Modules) {
    "use strict";
    let _defaultOptions = {
        minuteSpanHook: "",
        secoundHook: "",
        rememberTime: false
    };
    class TimerModule {
        //************************************************************
        //* Ctor
        //************************************************************
        constructor(options) {
            this._minsSpan = null;
            this._secsSpan = null;
            this._hsSpan = null;
            this._crrntDisplayedHs = null;
            this._crrntDisplayedSec = null;
            this._crrntDisplayedMin = null;
            this._startTime = null;
            this._animFrameReqId = null;
            this._options = Object.assign({}, _defaultOptions, options);
            this._startTime = null;
            this._totalMS = 0;
            this._kill = false;
        }
        //************************************************************
        //* Public member functions
        //************************************************************
        init(moduleHtml) {
            console.log("Timer module initialized");
            this._minsSpan = moduleHtml.querySelector(`[data-js-hook="tc:mins"]`);
            this._secsSpan = moduleHtml.querySelector(`[data-js-hook="tc:secs"]`);
            this._hsSpan = moduleHtml.querySelector(`[data-js-hook="tc:hs"]`);
            if (this._options.rememberTime) {
                let storedTime = window.localStorage.getItem(TimerModule._STORED_ST_KEY);
                if (typeof storedTime === "string") {
                    let storedST = parseFloat(storedTime);
                    this._startTime = isNaN(storedST) ? null : storedST;
                }
            }
        }
        onLoad() {
            if (this._minsSpan != null) {
                this._minsSpan.innerText = "00";
            }
            if (this._secsSpan != null) {
                this._secsSpan.innerText = "00";
            }
            if (this._hsSpan != null) {
                this._hsSpan.innerText = "00";
            }
            // start the timer. 
            this._animFrameReqId = requestAnimationFrame(this._tick.bind(this));
            console.log("Timer module loaded and started");
        }
        dispose() {
            console.log("Timer module disposed");
            this._kill = true;
            if (this._animFrameReqId != null) {
                cancelAnimationFrame(this._animFrameReqId);
            }
        }
        //************************************************************
        //* Private member functions
        //************************************************************
        /**
         *
         * @param deltaTime
         * @private
         */
        _tick(ts) {
            if (this._startTime == null) {
                this._startTime = ts;
                if (this._options.rememberTime) {
                    localStorage.setItem(TimerModule._STORED_ST_KEY, this._startTime.toString(10));
                }
                console.log("StartTime: ", this._startTime);
            }
            if (this._kill === false) {
                this._animFrameReqId = requestAnimationFrame(this._tick.bind(this));
            }
            this._totalMS = ts - this._startTime;
            let time = this._calculateTime();
            this._displayTime(time.min, time.sec, time.hs);
        }
        /**
         *
         * @return {{sec: number, min: number}}
         * @private
         */
        _calculateTime() {
            let totalHs = Math.floor(this._totalMS / 10), totalSeconds = Math.floor(this._totalMS / 1000), hs = totalHs % 100, seconds = totalSeconds % 60, minutes = Math.floor(totalSeconds / 60);
            return {
                sec: seconds,
                min: minutes,
                hs: hs
            };
        }
        /**
         *
         * @private
         */
        _displayTime(minutes, seconds, hs) {
            // TODO: Use regex instead to replace. 
            // don't update if the current displaed is the same as the give minutes parameter. 
            if (this._crrntDisplayedMin != minutes && this._minsSpan != null) {
                this._crrntDisplayedMin = minutes;
                this._minsSpan.innerText = minutes < 10 ? `0${minutes.toString(10)}` : minutes.toString(10);
            }
            // don't update if the current displaed is the same as the give seconds parameter. 
            if (this._crrntDisplayedSec != seconds && this._secsSpan != null) {
                this._crrntDisplayedSec = seconds;
                this._secsSpan.innerText = seconds < 10 ? `0${seconds.toString(10)}` : seconds.toString(10);
            }
            if (this._hsSpan != null && this._crrntDisplayedHs != hs) {
                this._crrntDisplayedHs = hs;
                this._hsSpan.innerText = hs < 10 ? `0${hs.toString(10)}` : hs.toString(10);
            }
        }
    }
    //********************************************************************************
    //** STATIC FIELDS
    //********************************************************************************
    TimerModule._STORED_ST_KEY = "timermodule/starttime";
    Modules.TimerModule = TimerModule;
})(Modules || (Modules = {}));
