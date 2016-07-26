import javax.swing.JFrame;
import java.awt.Color;

public class CalculatorViewer {

	public static void main(String[] args)
	{ 
		JFrame frame = new CalculatorFrame();
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.setTitle("Calculator");

		frame.setVisible(true);
	}

}
