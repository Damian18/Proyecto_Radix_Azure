var tour = {
    id: 'hello-hopscotch',
    steps: [
        {
            target: 'paso1',
            title: 'Esto es el cerrar sesion',
            content: 'Hey there! This is an example Hopscotch tour. There will be plenty of time to read documentation and sample code, but let\'s just take some time to see how Hopscotch actually works.',
            placement: 'left',
            yOffset: 1,
            arrowOffset: 1
          

        },
        {
            target: 'paso2',
            title: 'Notificaciones',
            content: 'At the very least, you\'ll need to include these two files in your project to get started.',
            placement: 'left',
            yOffset: 1,
            arrowOffset: 1
        },
        {
            target: 'paso3',
            placement: 'bottom',
            title: 'Tus sucursales',
            content: 'Once you have Hopscotch on your page, you\'re ready to start making your tour! The biggest part of your tour definition will probably be the tour steps.'
        },
        {
            target: 'paso4',
            placement: 'right',
            title: 'Otras Sucursales',
            content: 'After you\'ve created your tour, pass it in to the startTour() method to start it.',
            yOffset: -25
        },
        {
            target: 'paso5',
            placement: 'top',
            title: 'Basic step options',
            content: 'These are the most basic step options: <b>target</b>, <b>placement</b>, <b>title</b>, and <b>content</b>. For some steps, they may be all you need.',
            arrowOffset: 100
        },
        {
            target: 'paso6',
            placement: 'top',
            title: 'Hopscotch API methods',
            content: 'Control your tour programmatically using these methods.',
        },
        {
            target: 'paso7',
            placement: 'top',
            title: 'This tour\'s code',
            content: 'This is the JSON for the current tour! Pretty simple, right?',
        },
        {
            target: 'paso1',
            placement: 'bottom',
            title: 'Estas listo!!',
            content: 'Now go and build some great tours!',
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
