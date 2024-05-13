export class Timer {
    private startTime: number = 0;
    private remaining: number = 0;
    private paused: boolean = false;
    private timerId: NodeJS.Timeout | null = null;
    private readonly _function: Function;
    private readonly _delay: number;
    
    public constructor(func: Function, delay: number) {
        this._function = func;
        this._delay = delay;
    }
    
    public pause(): void {
        if(this.paused) return;
        this.clear();
        this.remaining = new Date().getTime() - this.startTime!;
        this.paused = true;
    }
    
    private clear(): void {
        clearInterval(this.timerId!)
    }
    
    public resume(): void {
        if(!this.paused) return;
        if(this.remaining){
            setTimeout(() => {
                this.run();
                this.paused = false;
                this.start();
            }, this.remaining);
        } else {
            this.paused = false;
            this.start();
        }
    }
    
    public start(): void {
        this.clear();
        this.timerId = setInterval(() => {
            this.run();
        }, this._delay)
    }
    
    private run(): void {
        this.startTime = new Date().getTime();
        this._function();
    }
    
}