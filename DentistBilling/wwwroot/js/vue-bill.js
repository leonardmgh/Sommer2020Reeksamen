vm = new Vue({
    el: '#vueroot',
    data: function () {
        return {
            billableItems: [],
            addedItems: [],
            currentIten: null,
            currentItemIndex : null,
            selectedItem : null,
            errorMessage: null,
            successMessage: null,
            isLoading: false,
            query: null,
            
        }
    },
    watch: {
    },
    computed: {
        filteredItems() {
            if (this.query == null || this.query === "") return this.billableItems;

            return this.billableItems.filter(billableItems => {
                return billableItems.Description.toLowerCase().indexOf(this.query.toLowerCase()) !== -1;
            });
        }
    },
    methods: {
        update() {
            this.isLoading = true;

            fetch('/api/BillableItems/',
                {
                    method: 'GET',
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    })
                }).then(function (response) {
                    vm.isLoading = false;

                    if (response.status < 200 || response.status >= 300) {
                        vm.errorMessage = 'An error occured: Status: ' + response.status;
                        return;
                    }

                    response.json().then(function (data) {
                        vm.billableItems = data;
                    }).catch(function (err) {
                        vm.errorMessage = 'Recieve Error: ' + err;
                    });
                });
        },

        onItemClick(index) {
            this.currentIten = this.billableItems[index]
            $('#billableItes').modal({ show: false })
            this.addedItems.push(billableItems[index])
        },

        addBill() {
            console.log("save sensor method");

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

        showBillableItems() {
            $('#billableItes').modal({ show: true })
        },

        showDetails(id) {
            fetch('/api/BillableItems/' + id,
                {
                    method: 'GET',
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    })
                }).then(function (response) {
                    vm.isLoading = false;

                    if (response.status < 200 || response.status >= 300) {
                        vm.errorMessage = 'An error occured: Status: ' + response.status;
                        return;
                    }

                    response.json().then(function (data) {
                        vm.currentIten = data;
                        console.log(data);
                    }).catch(function (err) {
                        vm.errorMessage = 'Fetch fejl: ' + err;
                    });
                });

            $('#detailsModal').modal({ show: true });
        },
    },
    mounted: function mounted() {
        this.update();
    }
});
