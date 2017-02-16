$(function() {
    var spEditor = {
        variables: {
            blank_uid: '00000000-0000-0000-0000-000000000000',
            base_path: window.base_path,
            query_api_result: [
                'Success',
                'Source Not Found',
                'Invalid Supplied Data',
                'Data Conflict'
            ],
            query_api_message: [
                '',
                'Can not bind to the Application. Since we can not find your ApplicationName',
                'Please fill all supplement data.',
                'Can not use Query name which allready exists in the current application.'
            ],
            query_list_colors: [ 'default', 'primary', 'success', 'info', 'warning', 'danger' ]
        },
        register: function() {
            spEditor.ui.register.apply();
            spEditor.events.register.apply();

            $('a[data-app-add]').trigger('click');
        },
        ui: {
            register: function() {
                spEditor.ui.applicationListBuilder.apply();
            },
            applicationListBuilder: function() {
                spEditor.common.crud.application.gets(function(r) {
                    var appContainer = $('ul.page-sidebar-menu');
                    var str = '';

                    $.each(r, function (key, val) {
                        var sTemplate = '<li class="start"><a data-id="{{id}}"><span class="title">{{name}}</span></a></li>';
                        str += _o_.string.template(sTemplate, {
                            id: val.Id,
                            name: val.Name
                        });
                    });

                    appContainer.fadeOut('fast', function() {
                        appContainer
                            .html(str)
                            .fadeIn('fast', function() {
                                spEditor.events.applicationList.selectApplication.apply();
                            });
                    });
                });
            },
            applicationListRemover: function(uid) {
                var appItem = $(_o_.string.format('a[data-id="{1}"]', uid), $('ul.page-sidebar-menu')).parents('li');

                appItem.fadeOut('slow', function() {
                    appItem.find(_o_.string.format('li[data-id="{1}"]', uid)).remove();
                    $('a[data-app-add]').trigger('click');
                });
            },
            applicationListSetActive: function() {
                $('li.start', $('ul.page-sidebar-menu')).removeClass('active');
                var item = $('li', $('ul.page-sidebar-menu'));

                $.each(item, function(key, val) {
                    if ($(val).text() === $('input#Name').val())
                        $(val).addClass('active');
                });
            },
            queryListBuilder: function () {
                function buildList(data) {
                    var queryContainer = $('div#query-list-container');
                    var shuffledColor = _o_.array.shuffle(spEditor.variables.query_list_colors);
                    var colorId = 0;
                    var str = '';

                    $.each(data, function (key, val) {
                        var strTemplate = '<div class="row margin-bottom-15"><div class="col-md-12"><div class="note note-{{colorId}}"><h4><strong>{{queryName}}</strong></h4><p>{{queryContent}}</p></div></div></div>';

                        str += _o_.string.template(strTemplate, {
                            colorId: shuffledColor[colorId],
                            queryName: val.Name,
                            queryContent: val.Query
                        });

                        colorId = colorId + 1 == data.length ? 0 : colorId + 1;
                    });

                    queryContainer.fadeOut('fast', function () {
                        queryContainer
                            .html(str)
                            .fadeIn('fast');
                    });
                }

                var appId = $('input#Id').val();
                if (appId === spEditor.variables.blank_uid)
                    buildList([]);

                else
                    spEditor.common.crud.query.gets(function(r) {
                        buildList(r);
                    });
            },
            queryDialog: {
                open: function(data) {
                    $('a[href="#tab_query_editor"]').trigger('click');
                    spEditor.common.setQueryForm(data);
                    $('div#QueryModal').modal('show');
                },
                close: function() {
                    $('div#QueryModal').modal('hide');
                }
            }
        },
        events: {
            register: function() {
                spEditor.events.applicationList.register.apply();
                spEditor.events.applicationForm.register.apply();
                spEditor.events.query.register.apply();
            },
            applicationList: {
                register: function() {
                    spEditor.events.applicationList.addButton.apply();
                },
                addButton: function() {
                    $('a[data-app-add]').on('click', function () {
                        spEditor.common.setApplicationForm({
                            Id: spEditor.variables.blank_uid,
                            Name: '',
                            ConnectionString: '',
                            IsActive: true
                        });

                        spEditor.ui.queryListBuilder.apply();
                    });
                },
                selectApplication: function() {
                    $('ul.page-sidebar-menu a').on('click', function() {
                        spEditor.common.crud.application.get($(this).data('id'), function(r) {
                            spEditor.common.setApplicationForm(r);
                            spEditor.ui.applicationListSetActive();

                            spEditor.ui.queryListBuilder.apply();
                        });
                    });
                }
            },
            applicationForm: {
                register: function() {
                    spEditor.events.applicationForm.btnDelete.apply();
                    spEditor.events.applicationForm.btnSave.apply();
                    spEditor.events.applicationForm.txtApplicationName.apply();
                    spEditor.events.applicationForm.cboIsActive.apply();
                },
                btnDelete: function() {
                    $('a[data-app-delete]').on('click', function() {
                        var uid = $('input#Id').val();

                        if (uid === spEditor.variables.blank_uid) {
                            bootbox.dialog({
                                message: 'Please select the Application first.',
                                title: 'Info',
                                buttons: {
                                    OK: {
                                        label: 'OK',
                                        className: 'blue'
                                    }
                                }
                            });

                        } else {
                            bootbox.dialog({
                                message: 'Are you sure want to delete this Application?<br/><br/><strong>WARNING :</strong> Deleting application will make the related queries will be deleted too.',
                                title: 'Delete Confirmation',
                                buttons: {
                                    DeleteAll: {
                                        label: 'Yes, Delete All',
                                        className: 'red',
                                        callback: function() {
                                            spEditor.common.crud.application.delete(uid, function() {
                                                spEditor.ui.applicationListRemover(uid);
                                            });
                                        }
                                    },
                                    Cancel: {
                                        label: 'No',
                                        className: 'blue'
                                    }
                                }
                            });
                        }
                    });
                },
                btnSave: function() {
                    $('a[data-app-save]').on('click', function() {
                        if (spEditor.common.applicationFormIsValid() == false) {
                            return;

                        } else {
                            var ser = $('form#formApplication').serialize();

                            if ($('input#Id', $('form#formApplication')).val() === spEditor.variables.blank_uid)
                                spEditor.common.crud.application.post(
                                    ser,
                                    function(r) {
                                        var sTemplate = '<li class="start"><a data-id="{{id}}"><span class="title">{{name}}</span></a></li>';
                                        $('ul.page-sidebar-menu').prepend(
                                            _o_.string.template(
                                                sTemplate,
                                                {
                                                    id: r.Supplement.Id,
                                                    name: r.Supplement.Name
                                                }
                                            ));
                                        $('input#Id', $('form#formApplication')).val(r.Supplement.Id);

                                        spEditor.ui.applicationListSetActive();
                                    }
                                );
                            else
                                spEditor.common.crud.application.put(
                                    ser,
                                    function(r) {
                                        $(_o_.string.format('a[data-id="{1}"]', r.Supplement.Id), $('ul.page-sidebar-menu'))
                                            .text(r.Supplement.Name);
                                    }
                                );
                        }
                    });
                },
                txtApplicationName: function() {
                    $('input[data-name]').on('keyup, keydown', function() {
                        $('span#applicationNameTitle').text($(this).val());
                    });
                },
                cboIsActive: function() {
                    $('input#IsActive').on('switchChange.bootstrapSwitch', function(event, state) {
                        if (state)
                            $(this)
                                .attr('checked', 'checked')
                                .val(true);
                        else
                            $(this)
                                .removeAttr('checked')
                                .val(false);
                    });
                }
            },
            query: {
                register: function() {
                    spEditor.events.query.addButton.apply();
                    spEditor.events.query.btnSave.apply();
                    spEditor.events.query.btnTestQuery.apply();
                },
                addButton: function() {
                    $('a#btnAddQuery').on('click', function() {
                        var appId = $('input#Id').val();

                        if (appId === spEditor.variables.blank_uid)
                            bootbox.dialog({
                                message: 'Please select the Application first.',
                                title: 'Info',
                                buttons: {
                                    OK: {
                                        label: 'OK',
                                        className: 'blue'
                                    }
                                }
                            });

                        else
                            spEditor.ui.queryDialog.open({
                                Id: spEditor.variables.blank_uid,
                                ApplicationId: $('input#Id').val(),
                                IsActive: true,
                                Name: '',
                                Query: ''
                            });
                    });
                },
                btnSave: function() {
                    $('button[name="save"]', $('div#QueryModal')).on('click', function () {
                        if (spEditor.common.queryFormIsValid() == false) {
                            return;

                        } else {
                            var frm = $('form#formQuery');
                            var ser = frm.serialize();
                            var isPost = $('input[name="Id"]', frm).val() == spEditor.variables.blank_uid;
                            
                            if (isPost) {
                                console.log(ser);
                                spEditor.common.crud.query.post(
                                    ser,
                                    function(r) {
                                        var strTemplate = '<div class="row margin-bottom-15"><div class="col-md-12"><div class="note note-{{colorId}}"><h4><strong>{{queryName}}</strong></h4><p>{{queryContent}}</p></div></div></div>';
                                        $('div#query-list-container').prepend(
                                            _o_.string.template(
                                                strTemplate,
                                                {
                                                    colorId: _o_.array.takeRandom(spEditor.variables.query_list_colors, 1),
                                                    queryName: r.Supplement.Name,
                                                    queryContent: r.Supplement.Query
                                                }
                                            )
                                        );
                                    }
                                );
                            } else {
                                alert('put update action here.');
                            }
                        }
                    });
                },
                // TODO: UNFINISHED
                btnTestQuery: function() {
                    $('button[name="test_query"]', $('div#QueryModal')).on('click', function () {
                        alert('test query');
                    });
                }
            }
        },
        common: {
            setApplicationForm: function(data) {
                $('span#applicationNameTitle').text(data.Name);

                var frm = $('form#formApplication');

                $.each(data, function(key, val) {
                    var cobj = $(_o_.string.format('#{1}', key), frm);

                    if (typeof (val) === 'boolean')
                        cobj.bootstrapSwitch('state', val);

                    else
                        cobj.val(val);
                });

                $("#Name", frm).focus();
            },
            applicationFormIsValid: function() {
                var isValid = true;

                $.each($('input[type="text"], textarea', $('form#formApplication')), function(key, val) {
                    if (!$(val).val()) {
                        isValid = false;

                        $(val).pulsate({
                            color: "#cb5a5e",
                            reach: 50,
                            repeat: 20,
                            speed: 50,
                            glow: true
                        });
                    }
                });

                if (!isValid) {
                    spEditor.common.toastr({
                        title: 'Invalid Value',
                        text: 'Blinked controls is not valid. Please set those controls value.'
                    });
                }

                return isValid;
            },
            setQueryForm: function(data) {
                var frm = $('form#formQuery');

                $.each(data, function(key, val) {
                    var cobj = $(_o_.string.format('[name="{1}"]', key), frm);

                    if (typeof(val) === 'boolean')
                        cobj.bootstrapSwitch('state', val);

                    else
                        cobj.val(val);
                });

                setTimeout(function() {
                    $('[name="Name"]', frm).focus();
                }, 750);
            },
            queryFormIsValid: function() {
                var isValid = true;

                $.each($('input[type="text"], textarea', $('form#formQuery')), function (key, val) {
                    if (!$(val).val()) {
                        isValid = false;

                        $(val).pulsate({
                            color: "#cb5a5e",
                            reach: 50,
                            repeat: 20,
                            speed: 50,
                            glow: true
                        });
                    }
                });

                if (!isValid) {
                    spEditor.common.toastr({
                        title: 'Invalid Value',
                        text: 'Blinked controls is not valid. Please set those controls value.'
                    });
                }

                return isValid;
            },
            toastr: function(option) {
                option = $.extend({
                    title: "your title",
                    text: "your text",
                    type: 'error'
                }, option);

                toastr.options = {
                    closeButton: true,
                    debug: false,
                    positionClass: "toast-bottom-right",
                    showEasing: 'swing',
                    hideEasing: 'linear',
                    showMethod: 'fadeIn',
                    hideMethod: 'fadeOut',
                    showDuration: 1000,
                    hideDuration: 1000,
                    timeOut: 5000,
                    extendedTimeOut: 5000,
                    onclick: null
                };

                toastr[option.type](option.text, _o_.string.format('<strong class="uppercase">{1}</strong><hr />', option.title));
            },
            ajax: function(options) {
                var defaultOptions = $.extend({
                    dataType: 'json',
                    error: function(x, s) {
                        spEditor.common.toastr({
                            title: _o_.string.format('<strong class="uppercase">{1}</strong><hr />', s),
                            text: JSON.parse(x.responseText).Message
                        });
                    }
                }, options);

                $.ajax(defaultOptions);
            },
            crud: {
                application: {
                    gets: function(callback) {
                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/application', spEditor.variables.base_path),
                            success: function(r) {
                                if (callback) callback(r);
                            }
                        });
                    },
                    get: function(uid, callback) {
                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/application/{2}', spEditor.variables.base_path, uid),
                            success: function(r) {
                                if (callback) callback(r);
                            }
                        });
                    },
                    post: function(data, callback) {
                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/application', spEditor.variables.base_path),
                            method: 'POST',
                            data: data,
                            success: function(r) {
                                if (r.Result == 0) {
                                    spEditor.common.toastr({
                                        title: 'Success',
                                        text: 'Data successfully saved.',
                                        type: 'success'
                                    });

                                    if (callback) callback(r);
                                }

                                if (r.Result == 3) {
                                    spEditor.common.toastr({
                                        title: 'Data Conflict',
                                        text: 'ApplicationName already exist, please use another name.'
                                    });
                                }
                            }
                        });
                    },
                    put: function(data, callback) {
                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/application', spEditor.variables.base_path),
                            method: 'PUT',
                            data: data,
                            success: function(r) {
                                if (r.Result == 0) {
                                    spEditor.common.toastr({
                                        title: 'Success',
                                        text: 'Data successfully updated.',
                                        type: 'success'
                                    });

                                    if (callback) callback(r);
                                }

                                if (r.Result == 3) {
                                    spEditor.common.toastr({
                                        title: 'Data Conflict',
                                        text: 'ApplicationName already exist, please use another name.'
                                    });
                                }
                            }
                        });
                    },
                    delete: function(uid, callback) {
                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/application/{2}', spEditor.variables.base_path, uid),
                            method: 'DELETE',
                            success: function(r) {
                                if (r.Result == 0) {
                                    spEditor.common.toastr({
                                        title: 'Success',
                                        text: 'Data successfully deleted.',
                                        type: 'success'
                                    });

                                    if (callback) callback(r);
                                }
                            }
                        });
                    }
                },
                query: {
                    gets: function (callback) {
                        var appId = $('input#Id', $('form#formApplication')).val();

                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/query', spEditor.variables.base_path),
                            data: {
                                appId: appId
                            },
                            success: function(r) {
                                if (callback) callback(r);
                            }
                        });
                    },
                    get: function(uid, callback) {
                        var appId = $('input#Id', $('form#formApplication')).val();

                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/query/{2}/{3}', spEditor.variables.base_path, appId, uid),
                            success: function(r) {
                                if (callback) callback(r);
                            }
                        });
                    },
                    post: function(data, callback) {
                        var appId = $('input#Id').val();
                        data.applId = appId;

                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/query/{2}', spEditor.variables.base_path, appId),
                            method: 'POST',
                            data: data,
                            success: function (r) {
                                if (r.Result == 0) {
                                    spEditor.common.toastr({
                                        title: 'Success',
                                        text: 'Data successfully saved.',
                                        type: 'success'
                                    });

                                    if (callback) callback(r);

                                } else {
                                    spEditor.common.toastr({
                                        title: spEditor.variables.query_api_result[r.Result],
                                        text: spEditor.variables.query_api_message[r.Result]
                                    });
                                }
                            }
                        });
                    },
                    put: function(data, callback) {
                        var appId = $('input#Id', $('form#formApplication')).val();

                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/query/{2}', spEditor.variables.base_path, appId),
                            method: 'PUT',
                            data: data,
                            success: function (r) {
                                if (r.Result == 0) {
                                    spEditor.common.toastr({
                                        title: 'Success',
                                        text: 'Data successfully updated.',
                                        type: 'success'
                                    });

                                    if (callback) callback(r);

                                } else {
                                    spEditor.common.toastr({
                                        title: spEditor.variables.query_api_result[r.Result],
                                        text: spEditor.variables.query_api_message[r.Result]
                                    });
                                }
                            }
                        });
                    },
                    delete: function(uid, callback) {
                        var appId = $('input#Id', $('form#formApplication')).val();

                        spEditor.common.ajax({
                            url: _o_.string.format('{1}api/query/{2}/{3}', spEditor.variables.base_path, appId, uid),
                            method: 'DELETE',
                            data: data,
                            success: function (r) {
                                if (r.Result == 0) {
                                    spEditor.common.toastr({
                                        title: 'Success',
                                        text: 'Data successfully deleted.',
                                        type: 'success'
                                    });

                                    if (callback) callback(r);

                                } else {
                                    spEditor.common.toastr({
                                        title: spEditor.variables.query_api_result[r.Result],
                                        text: spEditor.variables.query_api_message[r.Result]
                                    });
                                }
                            }
                        });
                    }
                }
            }
        }
    };

    spEditor.register.apply();
});