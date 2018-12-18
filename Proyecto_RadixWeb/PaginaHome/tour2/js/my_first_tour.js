var tour = {
    id: 'hello-hopscotch',
    steps: [
        {
            target: 'paso1',
            title: 'Opciones de perfil',
            content: 'Aqui por ejemplo puede encontrar el boton para cerrar tu sesion',
            placement: 'left',
            yOffset: 1,
            arrowOffset: 1
         

        },
        {
            target: 'paso2',
            title: 'El estado actual de tu empresa',
            content: 'En esta seccion puedes ver los recursos que posees hasta el momento',
            placement: 'bottom',
            xOffset: 350,
            yOffset: 20,
            arrowOffset: 1
        },
        {
            target: 'paso3',
            placement: 'right',
            title: 'Tu informacion',
            content: 'Un resumen rapido de lo que tienes en tu empresa'
        },
        {
            target: 'paso4',
            placement: 'right',
            title: 'Home',
            content: 'Puedes volver a la pagina de inicio solo presionando este boton',
            yOffset: -25
        },
        {
            target: 'paso5',
            placement: 'right',
            title: 'Tus Sucursales',
            content: 'Puedes ver todas las sucursales que estas administrando',
        },
        {
            target: 'paso6',
            placement: 'right',
            title: 'Tus Cuentas',
            content: 'Controla todas las cuentas que tienen tus trabajadores',
        },
        {
            target: 'paso7',
            placement: 'right',
            title: 'Tus Trabajadores',
            content: 'Puedes ver todos los trabajadores que pertenecen a tu empresa o sucursales.',
        },
        {
            target: 'paso8',
            placement: 'right',
            title: 'Tus Sectores',
            content: 'Puedes ver todos los sectores independiente la sucursal al que pertenezca',
        },
        {
            target: 'paso9',
            placement: 'right',
            title: 'Todos los Cargos',
            content: 'Ingresa cargos generales.',
        },
        {
            target: 'paso10',
            placement: 'right',
            title: 'Documentos',
            content: 'Puedes subir documentos siempre y cuando consideres el limite que tiene tu plan.',
        },
        {
            target: 'paso11',
            placement: 'right',
            title: 'Solicitudes QR',
            content: 'Debes utilizar esta funcion siempre y cuando quieras incoporar codigos qr a tus trabajadores',
        },
        {
            target: 'paso1',
            placement: 'bottom',
            title: 'Estas listo!!',
            content: 'Ahora ya sabes lo basico, disfruta de RADIX!!',
            yOffset: 50,
            xOffset: -550,
            arrowOffset: 'center'

        }
    ],
    i18n: {
        nextBtn: "Siguiente",
        prevBtn: "Anterior",
        doneBtn: "Terminar",
        skipBtn:"Saltar Paso"
    },
    showPrevButton: true,
    scrollTopMargin: 100
},

    /* ========== */
    /* TOUR SETUP */
    /* ========== */
    addClickListener = function (el, fn) {
        if (el.addEventListener) {
            el.addEventListener('click', fn, false);
        }
        else {
            el.attachEvent('onclick', fn);
        }
    },

    init = function () {
        var startBtnId = 'startTourBtn',
            calloutId = 'startTourCallout',
            mgr = hopscotch.getCalloutManager(),
            state = hopscotch.getState();

        if (state && state.indexOf('hello-hopscotch:') === 0) {
            // Already started the tour at some point!
            hopscotch.startTour(tour);
        }
        else {
            // Looking at the page for the first(?) time.
            setTimeout(function () {
                mgr.createCallout({
                    id: calloutId,
                    target: startBtnId,
                    placement: 'right',
                    title: 'Ten un Turorial!!!',
                    content: 'Vamos !',
                    arrowOffset: 'center',
                    width: 240,

                });
            }, 100);
        }

        addClickListener(document.getElementById(startBtnId), function () {
            if (!hopscotch.isActive) {
                mgr.removeAllCallouts();
                hopscotch.startTour(tour);
            }
        });
    };

init();
