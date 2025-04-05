function rewireLoggingToElement(eleLocator, eleOverflowLocator, autoScroll) {
    ['log', 'debug', 'warn', 'error', 'info'].forEach(name => {
        const oldMethod = console[name];
        console[name] = function (...args) {
            const output = produceOutput(name, args);
            const eleLog = eleLocator();
            if (autoScroll) {
                const eleContainerLog = eleOverflowLocator();
                const isScrolledToBottom = eleContainerLog.scrollHeight - eleContainerLog.clientHeight <= eleContainerLog.scrollTop + 1;
                eleLog.appendChild(output);
                if (isScrolledToBottom) {
                    eleContainerLog.scrollTop = eleContainerLog.scrollHeight - eleContainerLog.clientHeight;
                }
            } else {
                eleLog.appendChild(output);
            }
            oldMethod.apply(console, args);
        };
    });

    function produceOutput(level, args) {
        const div = document.createElement('div');
        div.className = level;
        div.textContent = args.map(arg =>
            typeof arg === 'object' ? JSON.stringify(arg) : arg
        ).join(' ');
        return div;
    }
}