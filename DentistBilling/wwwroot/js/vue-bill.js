vm = new Vue({
    el: '#vueroot',
    data: function () {
        return {
            locations: [],
            errorMessage: null,
            successMessage: null,
            isLoading: false,
            activeLocation: null,
            query: null,
            sensor: {
                errorMessage: null,
                errors: [],
                model: {
                    species: null,
                    identification: null,
                    latitude: null,
                    longitude: null,
                    locationId: null
                }
            }
        }
    },
    watch: {
    },
    computed: {
        filteredLocations() {
            if (this.query == null || this.query === "") return this.locations;

            return this.locations.filter(location => {
                return location.name.toLowerCase().indexOf(this.query.toLowerCase()) !== -1;
            });
        }
    },
    methods: {
        update() {
            this.isLoading = true;

            fetch('/api/Locations/',
                {
                    method: 'GET',
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    })
                }).then(function (response) {
                    vm.isLoading = false;

                    if (response.status < 200 || response.status >= 300) {
                        vm.errorMessage = 'Der er sket en fejl: Status: ' + response.status;
                        return;
                    }

                    response.json().then(function (data) {
                        vm.locations = data;
                    }).catch(function (err) {
                        vm.errorMessage = 'Fetch fejl: ' + err;
                    });
                });
        },

        showDetails(id) {
            console.log("details method");
            this.isLoading = true;

            fetch('/api/Locations/' + id,
                {
                    method: 'GET',
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    })
                }).then(function (response) {
                    vm.isLoading = false;

                    if (response.status < 200 || response.status >= 300) {
                        vm.errorMessage = 'Der er sket en fejl: Status: ' + response.status;
                        return;
                    }

                    response.json().then(function (data) {
                        vm.activeLocation = data;
                        console.log(data);
                    }).catch(function (err) {
                        vm.errorMessage = 'Fetch fejl: ' + err;
                    });
                });

            $('#detailsModal').modal({ show: true });
        },

        showSensorModal(id) {
            console.log("sensor method");
            this.resetSensorForm();

            this.isLoading = true;

            fetch('/api/Locations/' + id,
                {
                    method: 'GET',
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    })
                }).then(function (response) {
                    vm.isLoading = false;

                    if (response.status < 200 || response.status >= 300) {
                        vm.errorMessage = 'Der er sket en fejl: Status: ' + response.status;
                        return;
                    }

                    response.json().then(function (data) {
                        vm.activeLocation = data;
                        console.log(data);
                    }).catch(function (err) {
                        vm.errorMessage = 'Fetch fejl: ' + err;
                    });
                });

            $('#createSensorModal').modal({ show: true });
        },

        getSensorErrors() {
            var errors = [];

            if (this.sensor.model.species === null) errors.push("Træart skal udfyldes.");

            if (this.sensor.model.identification === null) {
                errors.push("Identifikation skal udfyldes.");
            } else if (!this.sensor.model.identification.match(/^[0-9ABCDEF]{16}$/g)) {
                errors.push("Identifikation skal være hexidecimal på 16 karakterer.");
            }

            if (this.sensor.model.latitude === null) {
                errors.push("Breddegrad skal udfyldes.");
            } else if (isNaN(this.sensor.model.latitude) || this.sensor.model.latitude < -90 || this.sensor.model.latitude > 90) {
                errors.push("Breddegrad skal være decimaltal mellem -90.0 og 90.0.");
            }

            if (this.sensor.model.longitude === null) {
                errors.push("Længdegrad skal udfyldes.");
            } else if (isNaN(this.sensor.model.longitude) || this.sensor.model.longitude < -180 || this.sensor.model.longitude > 180) {
                errors.push("Længdegrad skal være decimaltal mellem -180.0 og 180.0.");
            }

            return errors;
        },

        saveSensor() {
            console.log("save sensor method");

            this.sensor.errors = this.getSensorErrors();

            if (this.sensor.errors.length > 0) {
                return;
            }

            this.sensor.model.locationId = this.activeLocation.id;

            this.isLoading = true;

            fetch('/api/Sensors/',
                {
                    method: 'POST',
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    }),
                    body: JSON.stringify(this.sensor.model)
                }).then(function (response) {
                    vm.isLoading = false;

                    if (response.status < 200 || response.status >= 300) {
                        vm.sensor.errorMessage = 'Der er sket en fejl: Status: ' + response.status;
                        return;
                    }

                    response.json().then(function (data) {
                        console.log(data);

                        for (var i = 0; i < vm.locations.length; i++) {
                            if (vm.locations[i].id == vm.activeLocation.id) {
                                vm.locations[i].sensors.push(data);
                            }
                        }

                        vm.successMessage = "Sensor oprettet til " + vm.activeLocation.name + "!";

                        $('#createSensorModal').modal('hide');

                    }).catch(function (err) {
                        vm.sensor.errorMessage = 'Fetch fejl: ' + err;
                    });
                });
        },

        resetSensorForm() {
            this.sensor.model.species = null;
            this.sensor.model.identification = null;
            this.sensor.model.latitude = null;
            this.sensor.model.longitude = null;
            this.sensor.errorMessage = null;
            this.sensor.errors = [];
        }
    },
    mounted: function mounted() {
        this.update();
    }
});
