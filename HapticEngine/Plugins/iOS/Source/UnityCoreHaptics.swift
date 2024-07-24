import Foundation
import CoreHaptics

@available(iOS 13.0, *)
@objc public class UnityCoreHaptics: NSObject {
    
    @objc static let shared = UnityCoreHaptics()
    
    static var _engine: CHHapticEngine!
	static var _continuousPlayer : CHHapticPatternPlayer!
	static var _engineStarted = false;
		
    static var Engine: CHHapticEngine! {
        get {
            if (_engine == nil) {
                CreateEngine()
            }
            if (!_engineStarted) {
                StartEngine()
            }
            return _engine
        }
    }
    
    static var _supportsHaptics: Bool?
    static var SupportsHaptics: Bool {
        get {
            if (_supportsHaptics == nil) {
                _supportsHaptics = CHHapticEngine.capabilitiesForHardware().supportsHaptics
            }
            return _supportsHaptics!
        }
    }
            
    /// - Tag: CreateEngine
    @objc public static func CreateEngine() {
        // Create and configure a haptic engine.
        if (_engineStarted)
        {
            return;
        }
		if(!SupportsHaptics)
		{
            return;
		}
		
        do {
            _engine = try CHHapticEngine()
        } catch let error {
            print("Engine Creation Error: \(error)")
            return
        }

        _engineStarted = true
		_engine.playsHapticsOnly = true;
        
        // The stopped handler alerts you of engine stoppage due to external causes.
        _engine.stoppedHandler = { reason in
            print("The engine stopped for reason: \(reason.rawValue)")
            switch reason {
            case .audioSessionInterrupt: print("Audio session interrupt")
            case .applicationSuspended: print("Application suspended")
            case .idleTimeout: print("Idle timeout")
            case .systemError: print("System error")
            case .notifyWhenFinished: print("Playback finished")
            @unknown default:
                print("Unknown error")
            }
            
            _engineStarted = false
        }
        
        // The reset handler provides an opportunity for your app to restart the engine in case of failure.
        _engine.resetHandler = {
			do
			{
				try _engine.start();
				_engineStarted = true;
			}
			catch
			{
				_engineStarted = false;
			}
        }
		
		do
		{
			try _engine.start();
			_engineStarted = true;
		}
		catch
		{
			_engineStarted = false;
		}		
    }
    
    static func StartEngine() {
        // Start haptic engine to prepare for use.
        do {
            try _engine.start()
            
            // Indicate that the next time the app requires a haptic, the app doesn't need to call engine.start().
            _engineStarted = true
        } catch let error {
            print("The engine failed to start with error: \(error)")
        }
    }
	
	@objc public static func StopEngine()
    {
        _engine.stop();
        _engineStarted = false;
    }
	    
    @objc public static func SupportsCoreHaptics() -> Bool {
        return SupportsHaptics
    }
    
    @objc public static func PlayTransientHaptic(intensity: Float, sharpness: Float) {
        //Debug(log: "Playing transient haptic")
        if (!SupportsHaptics)
        {
            return;
        }
        
        let clampedIntensity = Clamp(value: intensity, min: 0, max: 1);
        let clampedSharpness = Clamp(value: sharpness, min: 0, max: 1);
        
        let hapticIntensity = CHHapticEventParameter(parameterID: .hapticIntensity, value: clampedIntensity);
        let hapticSharpness = CHHapticEventParameter(parameterID: .hapticSharpness, value: clampedSharpness);
        let event = CHHapticEvent(eventType: .hapticTransient, parameters: [hapticIntensity, hapticSharpness], relativeTime: 0);
        
        do {
            let pattern = try CHHapticPattern(events: [event], parameters: []);
            let player = try Engine.makePlayer(with: pattern);
            try player.start(atTime: CHHapticTimeImmediate);
        }
        catch let error
        {
            print("Failed to play transient pattern: \(error.localizedDescription).");
        }
    }
    
    
    @objc public static func PlayContinuousHaptic(intensity: Float, sharpness: Float, duration: Double) {
        if (!SupportsHaptics)
        {
            return;
        }

        let clampedIntensity = Clamp(value: intensity, min: 0, max: 1);
        let clampedSharpness = Clamp(value: sharpness, min: 0, max: 1);

        let hapticIntensity = CHHapticEventParameter(parameterID: .hapticIntensity, value: clampedIntensity);
        let hapticSharpness = CHHapticEventParameter(parameterID: .hapticSharpness, value: clampedSharpness);
        let event = CHHapticEvent(eventType: .hapticContinuous, parameters: [hapticIntensity, hapticSharpness], relativeTime: 0, duration: duration);

        do
        {
            let pattern = try CHHapticPattern(events: [event], parameters: []);
            _continuousPlayer = try Engine.makePlayer(with: pattern);
            try _continuousPlayer.start(atTime: CHHapticTimeImmediate);
        }
        catch let error
        {
            print("Failed to play continuous pattern: \(error.localizedDescription).");
        }
    }

	@objc public static func StopContinuousHaptic()
	{
		if (!SupportsHaptics)
		{
			return;
		}

		if (!_engineStarted)
		{
			return;
		}
		if (_continuousPlayer == nil)
		{
			return;
		}

		do 
		{
			try _continuousPlayer.stop(atTime: CHHapticTimeImmediate)
			_engineStarted = false;
		}
		catch let error
		{
			 print("Error stopping the continuous haptic player: \(error)");
		}
	}
    
    //MARK: Helper functions
    static func Clamp(value: Float, min: Float, max: Float ) -> Float {
        if (value > max)
        {
            return max;
        }
        if (value < min)
        {
            return min;
        }
        return value;
    }
}
