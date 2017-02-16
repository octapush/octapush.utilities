/*
 octapushJS
 Author  : Fadhly Permata
 eMail   : fadhly.permata@gmail.com
 Url     : www.octapush.com

 === Credits ===
 Prime Developer            : Fadhly Permata (Octapush Team)
 Developer Team Supporter   : Adam Sumarna   (Octapush Team)

 === Contributors ===
 ... just type your name here after editing the script ...
 */
(function () {
    'use strict';

    var currentVersion = '1.4.31';

    //noinspection JSUnusedGlobalSymbols,SpellCheckingInspection,LocalVariableNamingConventionJS
    var _o_ = {
        // console.log(_o_.version())
        version: function () {
            return currentVersion;
        },
        copyrightToConsole: function () {
            var patternArt = [
                [0x5c],
                [0x1e, 0x3, 0x33, 0x3, 0x5],
                [0x1e, 0x3, 0x33, 0x3, 0x5],
                [0xa, 0x5, 0x6, 0x5, 0x3, 0x3, 0x8, 0x6, 0x3, 0x2, 0x1, 0x5, 0x3, 0x3, 0x3, 0x3, 0x5, 0x5, 0x4, 0x3, 0x6],
                [0x7, 0x3, 0x2, 0x3, 0x3, 0x3, 0x2, 0x3, 0x2, 0x7, 0x3, 0x3, 0x2, 0x3, 0x3, 0x3, 0x2, 0x3, 0x3, 0x3, 0x3, 0x3, 0x2, 0x3, 0x3, 0x3, 0x2, 0x3, 0x1, 0x4, 0x2],
                [0x5, 0x3, 0x3, 0x3, 0x2, 0x3, 0x8, 0x6, 0x3, 0x3, 0x3, 0x3, 0x3, 0x3, 0x3, 0x3, 0x2, 0x3, 0x3, 0x3, 0x2, 0x5, 0x6, 0x3, 0x3, 0x3, 0x2],
                [0x4, 0x3, 0x3, 0x3, 0x2, 0x3, 0x8, 0x3, 0x6, 0x3, 0x3, 0x3, 0x3, 0x3, 0x3, 0x3, 0x2, 0x3, 0x3, 0x3, 0x3, 0x6, 0x4, 0x3, 0x3, 0x3, 0x3],
                [0x3, 0x3, 0x3, 0x3, 0x2, 0x3, 0x3, 0x3, 0x2, 0x3, 0x6, 0x3, 0x3, 0x3, 0x3, 0x3, 0x2, 0x3, 0x3, 0x3, 0x3, 0x3, 0x5, 0x5, 0x3, 0x3, 0x3, 0x3, 0x4],
                [0x2, 0x3, 0x2, 0x3, 0x3, 0x3, 0x2, 0x3, 0x3, 0x7, 0x2, 0x3, 0x2, 0x1, 0x1, 0x2, 0x3, 0x3, 0x1, 0x3, 0x4, 0x3, 0x3, 0x3, 0x2, 0x3, 0x3, 0x3, 0x2, 0x3, 0x3, 0x3, 0x5],
                [0x2, 0x5, 0x6, 0x5, 0x6, 0x5, 0x4, 0x4, 0x2, 0x3, 0x2, 0x5, 0x7, 0x5, 0x1, 0x3, 0x3, 0x5, 0x4, 0x3, 0x4, 0x3, 0x5],
                [0x2b, 0x3, 0x2e],
                [0x2b, 0x3, 0x2e],
                [0x5c]
            ];

            var patternChar = [' ', '.'];

            var str = '';
            _o_.each(patternArt, function (key, value) {
                var charMode = true;
                str += '\n';
                _o_.each(value, function (idx, val) {
                    charMode = !charMode;
                    str += _o_.string.repeat(patternChar[charMode ? 1 : 0], val, '');
                });
            });
            var currentYear = _o_.datetime.year();
            var strCopyleft = _o_.string.format('Copyleft @ 2015{1} Fadhly Permata', currentYear == 2015 ? '' : _o_.string.concat(' - ', currentYear));
            str = _o_.string.format('octapushJS - Most complete JavaScript library\n{1}{2}{1}', _o_.string.repeat('_', 92), str);
            str += _o_.string.format('\n{2}{1}', strCopyleft, _o_.string.repeat(' ', 92 - strCopyleft.length, ''));

            console.log(str);
        },
        // overiding localization into ID
        //_o_.localization.datetime = {
        //    translator: 'Fadhly Permata',
        //    lang: 'ID',
        //    /* -------------------------- */
        //    dayName: {
        //        short: ['Mgu', 'Sen', 'Sel', 'Rab', 'Kms', 'Jum', 'Sab'],
        //        long: ['Minggu', 'Senin', 'Selasa', 'Rabu', 'Kamis', 'Jumat', 'Sabtu']
        //    },
        //    monthName: {
        //        short: ['Jan', 'Feb', 'Mar', 'Apr', 'Mei', 'Jun', 'Jul', 'Agu', 'Sep', 'Okt', 'Nov', 'Des'],
        //        long: ['Januari', 'Februari', 'Maret', 'April', 'Mei', 'Juni', 'Juli', 'Agustus', 'September', 'Oktober', 'November', 'Desember']
        //    }
        //};
        localization: {
            datetime: {
                translator: 'Fadhly Permata',
                lang: 'EN',
                /* -------------------------- */
                dayName: {
                    short: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
                    long: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
                },
                monthName: {
                    short: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                    long: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']
                }
            }
        },
        // octapushJS.noop();
        noop: function () {
        },
        // console.log(octapushJS.getType('Fadhly Permata'));
        getType: function (obj) {
            return null === obj
                ? obj + ''
                : (typeof obj).toString();
        },
        // console.log(octapushJS.ifNull(new String(), 'ABCD'))
        ifNull: function (obj, value) {
            return !_o_.compare.isNullOrEmpty(obj)
                ? obj
                : value;
        },
        // octapushJS.each('a,b,c,d'.split(','), function(key, value) { console.log(value); })
        each: function (obj, callback, args) {
            var value,
                i = 0,
                length = obj.length,
                isArray = _o_.array.isArray(obj);

            if (callback)
                if (args) {
                    if (isArray)
                        for (; i < length; i++) {
                            value = callback.apply(obj[i], args);
                            if (false === value) break;
                        }
                    else
                        for (i in obj) {
                            //noinspection JSUnfilteredForInLoop
                            value = callback.apply(obj[i], args);
                            if (false === value) break;
                        }
                }
                else {
                    if (isArray)
                        for (; i < length; i++) {
                            value = callback.call(obj[i], i, obj[i]);
                            if (false === value) break;
                        }
                    else
                        for (i in obj) {
                            //noinspection JSUnfilteredForInLoop
                            value = callback.call(obj[i], i, obj[i]);
                            if (false === value) break;
                        }
                }

            return obj;
        },
        // octapushJS.each('a,b,c,d'.split(','), function(key, value) { console.log(value); })
        forEach: function (obj, callback, args) {
            return _o_.each(obj, callback, args);
        },
        currentHost: function () {
            return location.host;
        },
        compare: {
            // console.log(octapushJS.isNullOrEmpty(new String()))
            isNullOrEmpty: function (obj) {
                return (
                    _o_.compare.isUndefined(obj)
                    || null === obj
                    || '' === obj
                    || 0x0 === obj.length
                );
            },
            // console.log(octapushJS.isEmpty(new String()))
            isEmpty: function (obj) {
                var name;

                //noinspection LoopStatementThatDoesntLoopJS
                for (name in obj)
                    return false;

                return true;
            },
            isUndefined: function (obj) {
                return obj === undefined;
            },
            isDefined: function (obj) {
                return obj !== undefined;
            },
            isFunction: function (obj) {
                return (
                    obj instanceof Function
                    ||
                    Object.prototype.toString().call(obj) === '[object Function]'
                );
            },
            isElement: function (obj) {
                return !!(obj && obj.nodeType === 1);
            },
            isNan: function (obj) {
                return isNaN(obj);
            },
            isBoolean: function (obj) {
                return obj === true || obj === false || Object.prototype.toString().call(obj) === '[object Boolean]';
            },
            isArray: function(obj){
                return _o_.array.isArray(obj);
            }
        },
        string: {
            // console.log(octapushJS.string.isEqual('fadhly', 'FADHLY', true))
            isEqual: function (str, search, caseSensitive) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? (_o_.ifNull(caseSensitive, false)
                    ? str == search
                    : str.toLowerCase() == search.toLowerCase())
                    : null;
            },
            // console.log(octapushJS.string.isContain('fadhly', 'fad'))
            isContain: function (str, search) {
                return _o_.compare.isNullOrEmpty(str)
                    ? false
                    : str.search(search) != -1;
            },
            // console.log(octapushJS.string.isAlpha('fadhly'))
            isAlpha: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? !/[^a-z\xDF-\xFF]|^$/.test(_o_.string.toLower(str))
                    : false;
            },
            // console.log(octapushJS.string.isAlphaNumeric('fadh325@yahoo.com'))
            isAlphaNumeric: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? !/[^0-9a-z\xDF-\xFF]/.test(_o_.string.toLower(str))
                    : false;
            },
            // console.log(octapushJS.string.isNumeric('130585'))
            isNumeric: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? !/[^0-9]/.test(str)
                    : null;
            },
            // console.log(octapushJS.string.isLower('FADHLY'))
            isLower: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? _o_.string.isAlpha(str) && str == _o_.string.toLower(str)
                    : false;
            },
            // console.log(octapushJS.string.isUpper('FADHLY'))
            isUpper: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? _o_.compare.isNullOrEmpty(str) && str == _o_.string.toUpper(str)
                    : false;
            },
            // console.log(_o.string.isStartsWith('permata', 'per'));
            isStartsWith: function (str, search) {
                var args = arguments;
                return !(_o_.compare.isNullOrEmpty(str) || _o_.compare.isNullOrEmpty(search))
                    ? (function () {
                    var suffixes = Array.prototype.slice.call(args, 0);
                    return _o_.string.left(suffixes[0], suffixes[1].length) == suffixes[1];
                })()
                    : false;
            },
            // console.log(octapushJS.string.isEndsWith('permata', 'ta'))
            isEndsWith: function (str, search) {
                var args = arguments;
                return !(_o_.compare.isNullOrEmpty(str) || _o_.compare.isNullOrEmpty(search))
                    ? (function () {
                    var suffixes = Array.prototype.slice.call(args, 0);
                    return _o_.string.right(suffixes[0], suffixes[1].length) == suffixes[1];
                })()
                    : false;
            },
            // console.log(octapushJS.string.format('HELLO {1}', 'Fadhly'))
            format: function (args) {
                var argue = arguments;
                return !_o_.compare.isNullOrEmpty(args)
                    ? (function () {
                    var formatPos = new RegExp('\{([1-' + argue.length + '])\}', 'g');
                    return String(args)
                        .replace(formatPos, function (key, value) {
                            return value >= argue.length ? key : argue[value];
                        });
                })()
                    : null;
            },
            //console.log(_o.string.template('Hello {{name}}! Do you will come to my party at {{party_plan}}?\nI\'ve inviting {{invited_name.c.a}} and {{invited_name.d.2}} too.', {
            //    name: 'Fadhly',
            //    party_plan: '2016-01-01',
            //    invited_name: {
            //        a: 'Rani',
            //        b: 'Haura',
            //        c: {
            //            a: 'Humaira',
            //            b: 'Damar'
            //        },
            //        d: ['Bangkit', 'Indah', 'Mega']
            //    }
            //}));
            template: function (str, values, opening, closing) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? !_o_.compare.isNullOrEmpty(values)
                    ? (function () {
                    opening = _o_.ifNull(opening, '{{');
                    closing = _o_.ifNull(closing, '}}');

                    var open = opening.replace(/[-[\]()*\s]/g, '\\$&').replace(/\$/g, '\\$');
                    var close = closing.replace(/[-[\]()*\s]/g, '\\$&').replace(/\$/g, '\\$');

                    var r = new RegExp(_o_.string.concat(open, '(.+?)', close), 'g');
                    var matches = str.match(r) || [];

                    _o_.each(matches, function (key, value) {
                        var sTpl = _o_.string.chomp(value, opening, closing);
                        var multiDim = sTpl.match(/(.)(\w+)/g);

                        var currentVal = values;
                        _o_.each(multiDim, function (idx, ival) {
                            ival = _o_.string.chompLeft(ival, '.');
                            currentVal = currentVal[ival.toString()];
                        });

                        str = _o_.string.replace(str, _o_.string.concat(opening, sTpl, closing), currentVal);
                    });

                    return str;
                })()
                    : str
                    : null;
            },
            // console.log(octapushJS.string.concat('FADHLY ', 13, 05, ' BIRTHDAY'));
            concat: function (args) {
                var argue = arguments;
                return 1 <= argue.length
                    ? (function () {
                    var result = '';

                    _o_.each(argue, function (key, value) {
                        result = _o_.string.format('{1}{2}', result, value);
                    });

                    return result;
                })()
                    : null;
            },
            // console.log(octapushJS.string.trim(' fadhly permata '))
            trim: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.replace(/(^\s*|\s*$)/g, '')
                    : null;
            },
            // console.log(octapushJS.string.trimLeft(' fadhly permata '))
            trimLeft: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.replace(/(^\s*)/g, '')
                    : null;
            },
            // console.log(octapushJS.string.trimRight(' fadhly permata '))
            trimRight: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.replace(/\s+$/, '')
                    : null;
            },
            // console.log(octapushJS.string.chomp('[[[fadhly permata]]]', '[[[', ']]]'));
            chomp: function (str, leftPrefix, rightPrefix) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? (!_o_.compare.isNullOrEmpty(leftPrefix) || !_o_.compare.isNullOrEmpty(rightPrefix))
                    ? (function () {
                    return _o_.string.chompRight(_o_.string.chompLeft(str, leftPrefix), rightPrefix);
                })()
                    : str
                    : null;
            },
            // console.log(octapushJS.string.chompLeft('[[[fadhly permata]]]', '[[['))
            chompLeft: function (str, prefix) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? !_o_.compare.isNullOrEmpty(prefix)
                    ? str.indexOf(prefix) === 0
                    ? str.slice(prefix.length)
                    : str
                    : str
                    : null;
            },
            // console.log(octapushJS.string.chompRight('[[[fadhly permata]]]', ']]]'))
            chompRight: function (str, suffix) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? !_o_.compare.isNullOrEmpty(suffix)
                    ? _o_.string.isEndsWith(str, suffix)
                    ? str.slice(0, str.length - suffix.length)
                    : str
                    : str
                    : null;
            },
            // console.log(octapushJS.string.pad('Permata', 11, '+'));
            pad: function (str, len, char) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.length < len
                    ? (function () {
                    char = _o_.ifNull(char, ' ');
                    len -= str.length;
                    var left = new Array(Math.ceil(len / 2) + 1).join(char);
                    var right = new Array(Math.floor(len / 2) + 1).join(char);
                    return _o_.string.format('{1}{2}{3}', left, str, right);
                })()
                    : str
                    : null;
            },
            // console.log(octapushJS.string.padLeft('Permata', 11, '+'));
            padLeft: function (str, len, char) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? len > str.length
                    ? _o_.string.format('{1}{2}', new Array(len - str.length + 1).join(_o_.ifNull(char, ' ')), str)
                    : str
                    : null;
            },
            // console.log(octapushJS.string.padRight('Permata', 11, '+'));
            padRight: function (str, len, char) {
                return _o_.compare.isNullOrEmpty(str)
                    ? null
                    : str.length >= len
                    ? str
                    : _o_.string.format('{1}{2}', str, [](len - str.length + 1).join(_o_.ifNull(char, ' ')));
            },
            // console.log(octapushJS.string.collapseWhitespace(' [ [ Permata ] ] '));
            collapseWhitespace: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.replace(/[\s\xa0]+/g, ' ').replace(/^\s+|\s+$/g, '')
                    : null;
            },
            // console.log(octapushJS.string.left('[[[fadhly permata]]]', 3))
            left: function (str, count) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.substring(0, parseInt(_o_.ifNull(count, 1)))
                    : null;
            },
            // console.log(octapushJS.string.right('[[[fadhly permata]]]', 3))
            right: function (str, count) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.substring(str.length - _o_.ifNull(count, 1))
                    : null;
            },
            // console.log(octapushJS.string.mid('[[[fadhly permata]]]', 3, 3))
            mid: function (str, left, right) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.substring(parseInt(_o_.ifNull(left, 1)), str.length - parseInt(_o_.ifNull(right, 1)))
                    : null;
            },
            // console.log(octapushJS.string.between(' [ [ Permata ] ] ', ' P', 'ta'));
            between: function (str, left, right) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? !(_o_.compare.isNullOrEmpty(left) || _o_.compare.isNullOrEmpty(right))
                    ? (function () {
                    var begPos = str.indexOf(left);
                    var endPos = str.indexOf(right, begPos + left.length);
                    return !(-1 == endPos && null !== right)
                        ? -1 == endPos && null === right
                        ? str.substring(begPos + left.length)
                        : str.slice(begPos + left.length, endPos)
                        : '';
                })()
                    : str
                    : null;
            },
            // console.log(octapushJS.string.charAtIndex('[[[fadhly permata]]]', 3))
            charAtIndex: function (str, index) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.charAt(parseInt(_o_.ifNull(index, 0)))
                    : null;
            },
            // console.log(octapushJS.string.toLower('[[[FADHLY PERMATA]]]'))
            toLower: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.toLowerCase()
                    : null;
            },
            // console.log(octapushJS.string.toUpper('[[[fadhly permata]]]'))
            toUpper: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.toUpperCase()
                    : null;
            },
            // console.log(octapushJS.string.capitalize('FADHLY HUMAIRA HAURA PERMATA', true));
            // console.log(octapushJS.string.capitalize('FADHLY HUMAIRA HAURA PERMATA'));
            capitalize: function (str, all) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? (function () {
                    var b;
                    all = _o_.ifNull(all, false);

                    return str.toLowerCase().replace(all
                            ? /[^']/g
                            : /^\S/, function (word) {
                            var d = word.toUpperCase(),
                                e;
                            e = b ? word : d;
                            b = d !== word;
                            return e
                        }
                    );
                })()
                    : null;
            },
            // console.log(octapushJS.string.repeat('Fadhly Permata', 5))
            // console.log(octapushJS.string.repeat('Fadhly Permata', 5, '###'))
            repeat: function (str, count, delimiter) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? new Array(parseInt(_o_.ifNull(count, 1) + 1)).join(_o_.string.format('{1}{2}', str, _o_.ifNull(delimiter, '')))
                    : null;
            },
            // console.log(octapushJS.string.times('Fadhly Permata', 5))
            // console.log(octapushJS.string.times('Fadhly Permata', 5, '###'))
            times: function (str, count, delimiter) {
                return _o_.string.repeat(str, count, delimiter);
            },
            // console.log(octapushJS.string.toHex('Fadhly Permata'))
            toHex: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? (function () {
                    var sHex = '';

                    _o_.each(str.split(''), function (key, value) {
                        sHex += _o_.string.format('\\x{1}', value.charCodeAt(0).toString(0x10).toUpperCase());
                    });

                    return sHex;
                })()
                    : null;
            },
            // console.log(octapushJS.string.fromHex('\x46\x61\x64\x68\x6C\x79\x20\x50\x65\x72\x6D\x61\x74\x61'))
            fromHex: function (str) {
                var args = arguments;
                return !_o_.compare.isNullOrEmpty(str)
                    ? (function () {
                    return str.replace(/\\x([0-9A-Fa-f]{2})/g, function () {
                        return String.fromCharCode(parseInt(args[1], 16));
                    });
                })()
                    : null;
            },
            // console.log(octapushJS.string.toUnicode('Fadhly Permata'))
            toUnicode: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? (function () {
                    var sUnc = '';
                    _o_.each(str.split(''), function (key, value) {
                        sUnc += _o_.string.format('\\u00{1}', value.charCodeAt(0).toString(0x10).toUpperCase());
                    });

                    return sUnc;
                })()
                    : null;
            },
            // console.log(octapushJS.string.fromUnicode('\u0046\u0061\u0064\u0068\u006C\u0079\u0020\u0050\u0065\u0072\u006D\u0061\u0074\u0061'))
            fromUnicode: function (str) {
                var args = arguments;
                return !_o_.compare.isNullOrEmpty(str)
                    ? (function () {
                    return str.replace(/\\u([0-9A-Fa-f]{2})/g, function () {
                        return String.fromCharCode(parseInt(args[1], 16));
                    });
                })()
                    : null;
            },
            // console.log(octapushJS.string.reverse('ATAMREP YLHDAF'));
            reverse: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? str.split('').reverse().join('')
                    : null;
            },
            // console.log(_o.string.toCharCodes('fadhly', function(str, charCode, counter) {
            //     console.log(_o.string.format('{1} --> {2} --> {3}', str, charCode, counter));
            // }));
            chars: function (str) {
                return _o_.compare.isNullOrEmpty(str)
                    ? null
                    : _o_.each(str);
            },
            toCharCodes: function (str, eachCallback) {
                return !(_o_.compare.isNullOrEmpty(str) || _o_.compare.isNullOrEmpty(eachCallback))
                    ? (function () {
                    var result = [];
                    var counter = 0;

                    for (var i = str.length; counter < i; i++) {
                        var charCode = str.charCodeAt(counter);
                        result.push(charCode);
                        eachCallback && eachCallback.call(str, charCode, counter);
                    }

                    return result;
                })()
                    : null;
            },
            // console.log(_o.string.shift('fadhly', 1));
            // console.log(_o.string.shift('fadhly', -1));
            shift: function (str, shiftCounter) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? (function () {
                    var result = '';
                    shiftCounter = shiftCounter || 0;
                    _o_.string.toCharCodes(str, function (char) {
                        result += String.fromCharCode(char + shiftCounter);
                    });

                    return result;
                })()
                    : null;
            },
            // console.log(_o.string.replace('fodhly permoto', 'o', 'a'))
            replace: function (str, oldChars, newChars) {
                return _o_.compare.isNullOrEmpty(str)
                    ? null
                    : _o_.compare.isNullOrEmpty(oldChars) || _o_.compare.isNullOrEmpty(newChars)
                    ? str
                    : str.replace(new RegExp(oldChars, 'g'), newChars);
            },
            toJson: function (str) {
                return !_o_.compare.isNullOrEmpty(str)
                    ? _o_.json.parse(str)
                    : {};
            }
        },
        datetime: {
            // console.log(_o.datetime.isLeapYear(2016));
            isLeapYear: function (year) {
                return (0 === year % 4 && 0 !== year % 100) || 0 === year % 400;
            },
            convertFromDotnetDate: function (dotnetDate) {
                return new Date(parseInt(dotnetDate.substr(6)));
            },
            // console.log(_o.datetime.now());
            now: function () {
                return +(new Date());
            },
            // console.log(_o.datetime.format(_o.datetime.now(), 'dddd, dd mmmm yyyy (hh:ii:ss)'));
            format: function (datetime, format) {
                datetime = new Date(_o_.ifNull(datetime, _o_.datetime.now()));
                format = _o_.ifNull(format, 'dddd, dd mmmm yyyy (hh:ii:ss)');

                var arrFormat = 'dddd,ddd,dd,mmmm,mmm,mm,yyyy,yy,hh,ii,ss'.split(',');
                var arrDt = [
                    _o_.localization.datetime.dayName.long[datetime.getDay()],
                    _o_.localization.datetime.dayName.short[datetime.getDay()],
                    _o_.number.zeroPad(datetime.getDate(), 2),
                    _o_.localization.datetime.monthName.long[datetime.getMonth()],
                    _o_.localization.datetime.monthName.short[datetime.getMonth()],
                    _o_.number.zeroPad(datetime.getMonth() + 1, 2),
                    datetime.getFullYear(),
                    _o_.string.right(datetime.getFullYear().toString(), 2),
                    _o_.number.zeroPad(datetime.getHours(), 2),
                    _o_.number.zeroPad(datetime.getMinutes(), 2),
                    _o_.number.zeroPad(datetime.getSeconds(), 2)
                ];

                _o_.each(arrFormat, function (key, val) {
                    format = _o_.string.replace(format, val, arrDt[key]);
                });

                return format;
            },
            //console.log(_o.datetime.toJson());
            //console.log(_o.datetime.toJson(_o.datetime.now()));
            toJson: function (datetime) {
                datetime = new Date(_o_.ifNull(datetime, _o_.datetime.now()));

                return {
                    year: datetime.getFullYear(),
                    longMonthName: _o_.localization.datetime.monthName.long[datetime.getMonth()],
                    shortMonthName: _o_.localization.datetime.monthName.short[datetime.getMonth()],
                    month: datetime.getMonth(),
                    longDayName: _o_.localization.datetime.dayName.long[datetime.getDay()],
                    shortDayName: _o_.localization.datetime.dayName.short[datetime.getDay()],
                    date: datetime.getDate(),
                    hour: datetime.getHours(),
                    minute: datetime.getMinutes(),
                    second: datetime.getSeconds(),
                    milliSecond: datetime.getMilliseconds()
                }
            },
            // console.log(_o.datetime.format(_o.datetime.shift(_o.datetime.now(), 'month', 2), 'dddd, dd mmmm yyyy (hh:ii:ss)'));
            shift: function (datetime, type, counter) {
                return _o_.compare.isNullOrEmpty(datetime) || _o_.compare.isNullOrEmpty(type) || _o_.compare.isNullOrEmpty(counter) ? new Date(datetime) : (function () {
                    datetime = new Date(datetime);

                    switch (type) {
                        case 'years':
                            datetime = new Date(datetime.setFullYear(datetime.getFullYear() + counter));
                            break;

                        case 'months':
                            datetime = new Date(datetime.setFullYear(datetime.getFullYear(), datetime.getMonth() + counter));
                            break;

                        case 'days':
                            datetime = new Date(datetime.setFullYear(datetime.getFullYear(), datetime.getMonth(), datetime.getDate() + counter));
                            break;

                        case 'hours':
                            datetime = new Date(datetime.setHours(datetime.getHours() + counter));
                            break;

                        case 'minutes':
                            datetime = new Date(datetime.setHours(datetime.getHours(), datetime.getMinutes() + counter));
                            break;

                        case 'seconds':
                            datetime = new Date(datetime.setHours(datetime.getHours(), datetime.getMinutes(), datetime.getSeconds() + counter));
                            break;

                        case 'milliseconds' || 'ms':
                            datetime = new Date(datetime.setHours(datetime.getHours(), datetime.getMinutes(), datetime.getSeconds(), datetime.getMilliseconds() + counter));
                            break;

                        default:
                            break;
                    }

                    return datetime;
                })();

            },
            year: function (datetime) {
                return _o_.datetime.toJson(datetime).year;
            },
            month: function (datetime) {
                return _o_.datetime.toJson(datetime).month;
            },
            date: function (datetime) {
                return _o_.datetime.toJson(datetime).date;
            },
            hour: function (datetime) {
                return _o_.datetime.toJson(datetime).hour;
            },
            minute: function (datetime) {
                return _o_.datetime.toJson(datetime).minute;
            },
            second: function (datetime) {
                return _o_.datetime.toJson(datetime).second;
            },
            milliSecond: function (datetime) {
                return _o_.datetime.toJson(datetime).milliSecond;
            },
            shortDayName: function (datetime) {
                return _o_.datetime.toJson(datetime).shortDayName;
            },
            longDayName: function (datetime) {
                return _o_.datetime.toJson(datetime).longDayName;
            },
            shortMonthName: function (datetime) {
                return _o_.datetime.toJson(datetime).shortMonthName;
            },
            longMonthName: function (datetime) {
                return _o_.datetime.toJson(datetime).longMonthName;
            }
        },
        array: {
            // console.log(octapushJS.array.isArray(new Array()))
            isArray: function (obj) {
                //return obj != undefined
                //    ? (function () {
                //    var type = _o_.getType(obj);
                //    var length = obj.length;
                //
                //    return _o_.string.isEqual('function', type) || _o_.string.isEqual('string', type)
                //        ? false
                //        : obj.nodeType === 1 && length
                //        ? true
                //        : _o_.string.isEqual('array', type) || length === 0 || _o_.string.isEqual(typeof length, 'number') && length > 0;
                //})()
                //    : false;
                if (_o_.compare.isUndefined(obj))
                    return false;
                else
                    return _o_.string.isEqual(_o_.getType(obj), 'object') || _o_.string.isEqual(_o_.getType(obj), 'array')
            },
            // console.log(_o_.array.count([1,2,3,4,5]));
            count: function (arr) {
                return !(_o_.compare.isNullOrEmpty(arr) || !_o_.array.isArray(arr))
                    ? arr.length
                    : 0;
            },
            // console.log(_o_.array.combine([1,2,3,4], [1,2,3,4]));
            combine: function (arr1, arr2) {
                return !_o_.compare.isNullOrEmpty(arguments) ? !_o_.compare.isNullOrEmpty(arr2)
                    ? arr1.concat(arr2)
                    : arr1
                    : [];
            },
            // console.log(_o_.array.pushAtIndex([ "fadhly", 1, 2, 3, 4], 's', 6));
            pushAtIndex: function (arr, newData, index) {
                if (_o_.compare.isNullOrEmpty(arguments)) {
                    return [];
                } else {
                    return !(!_o_.array.isArray(arr) || _o_.compare.isNullOrEmpty(newData))
                        ? (function () {
                        return !(index < 0 || index == _o_.array.count(arr))
                            ? arr.slice(0, index).concat(newData, arr.slice(index))
                            : arr.concat(newData);
                    })()
                        : arr;
                }
            },
            // console.log(_o_.array.pushFirst([ "fadhly", 1, 2, 3, 4], 's'));
            pushFirst: function (arr, newData) {
                return !_o_.compare.isNullOrEmpty(arguments)
                    ? !_o_.compare.isNullOrEmpty(newData)
                    ? (function () {
                    arr.unshift(newData);
                    return arr;
                })() //_o_.array.pushAtIndex(arr, newData, 0)
                    : arr
                    : [];
            },
            // console.log(_o_.array.pushLast(["fadhly", 1, 2, 3, 4], 's'));
            pushLast: function (arr, newData) {
                return !_o_.compare.isNullOrEmpty(arguments)
                    ? !_o_.compare.isNullOrEmpty(newData)
                    ? (function () {
                    arr.push(newData);
                    return arr;
                })()
                    : arr
                    : [];
            },
            //console.log(_o_.array.excludeFirst(["fadhly", 2, 3, 4, 1].sort(), 1));
            excludeFirst: function (arr, count) {
                return _o_.compare.isNullOrEmpty(arguments)
                    ? []
                    : (function () {
                    count = _o_.compare.isNullOrEmpty(count) || _o_.compare.isNan(count) ? 1 : count;
                    return arr.slice(count);
                })();
            },
            //console.log(_o_.array.excludeRest(["fadhly", 2, 3, 4, 1].sort(), 2));
            excludeRest: function (arr, count) {
                return _o_.compare.isNullOrEmpty(arguments)
                    ? []
                    : (function () {
                    count = _o_.compare.isNullOrEmpty(count) || _o_.compare.isNan(count) ? 1 : (arr.length <= count ? arr.length - 2 : count);
                    return arr.slice(0, arr.length - (count + 1));
                })();
            },
            //console.log(_o_.array.takeFirst(["fadhly", 2, 3, 4, 1].sort(), 2));
            takeFirst: function (arr, count) {
                return _o_.compare.isNullOrEmpty(arguments)
                    ? []
                    : (function () {
                    count = _o_.compare.isNullOrEmpty(count) || _o_.compare.isNan(count) ? 1 : arr.length - count - 1;
                    return _o_.array.excludeRest(arr, count);
                })();
            },
            //console.log(_o_.array.takeRest(["fadhly", 2, 3, 4, 1].sort(), 2));
            takeRest: function (arr, count) {
                return _o_.compare.isNullOrEmpty(arguments)
                    ? []
                    : (function () {
                    count = _o_.compare.isNullOrEmpty(count) || _o_.compare.isNan(count) ? 1 : arr.length - count;
                    return _o_.array.excludeFirst(arr, count);
                })();
            },
            // console.log(_o_.array.takeRandom(["fadhly", 2, 3, 4, 1].sort(), 3));
            takeRandom: function (arr, count) {
                return _o_.compare.isNullOrEmpty(arguments)
                    ? []
                    : (function () {
                    return _o_.array.takeFirst(_o_.array.shuffle(arr), count);
                })();
            },
            toString: function (data) {
                data = data || [];
                return JSON.stringify(data);
            },
            //console.log(_o_.array.shuffle(["fadhly", 2, 3, 4, 1].sort(), 2));
            shuffle: function (arr) {
                return _o_.compare.isNullOrEmpty(arguments)
                    ? []
                    : (function () {
                    return arr.sort(function () {
                        return 0.5 - Math.random();
                    });
                })();
            }
        },
        json: {
            // console.log(_o.json.parse('{"employee":[{"name":"fadhly"},{"name":"adam"}]}'));
            parse: function (data) {
                return !_o_.compare.isNullOrEmpty(data)
                    ? JSON.parse(_o_.string.concat(data, ''))
                    : {};
            },
            count: function (data) {
                return !_o_.compare.isNullOrEmpty(data)
                    ? (function () {
                    if (_o_.string.isEqual(typeof data, 'string'))
                        data = _o_.json.parse(data);

                    var counter = 0;
                    _o_.each(data, function () {
                        counter++
                    });
                    return counter;
                })()
                    : null;
            },
            toString: function (data) {
                return !(_o_.compare.isNullOrEmpty(data) || _o_.json.count(data) < 1)
                    ? JSON.stringify(data)
                    : '';
            }
        },
        collection: {},
        object: {},
        number: {
            //console.log(_o.number.zeroPad(30, 5));
            zeroPad: function (numb, lenOfNumber) {
                return !_o_.compare.isNullOrEmpty(numb)
                    ? !_o_.compare.isNullOrEmpty(lenOfNumber)
                    ? _o_.string.padLeft(numb.toString(), lenOfNumber, '0')
                    : numb
                    : null;
            }
        }
    };

    _o_.copyrightToConsole();
    window.octapushJS = window.octapush = window.octa = window._o_ = _o_;
})();