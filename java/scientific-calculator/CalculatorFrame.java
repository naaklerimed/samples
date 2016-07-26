import java.awt.BorderLayout;
import java.awt.GridLayout;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import javax.swing.ButtonGroup;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JLabel;
/**
   This frame contains a panel that displays buttons
   for a calculator and a panel with a text fields to
   specify the result of calculation.
 */
public class CalculatorFrame extends JFrame
{  
	private JLabel display;
	JMenuBar menubar,bar;
	JMenu help,file;
	JMenuItem about,reset,exit;
	private Object errormsg ="Press '=' before making a conversion";
	private double lastValue;
	private String lastOperator;
	private boolean startNewValue;
	private JMenu options;
	private JMenuItem Base;
	private JRadioButton Base2;
	private JRadioButton Base8;
	private JRadioButton Base10;
	private JRadioButton Base16;
	private JMenu choice;
	private ButtonGroup basegroup;
	private JRadioButton degree;
	private JRadioButton	radian;
	private ButtonGroup bgroup;

	public  String title = "About";
	private JPanel buttonPanel = new JPanel();
	private static final int FRAME_WIDTH = 400;
	private static final int FRAME_HEIGHT = 300;
	double value;
	public CalculatorFrame()
	{
		JMenuBar menubar = new JMenuBar();

		file = new JMenu("File");
		menubar.add(file);

		reset = new JMenuItem("Reset");
		file.add(reset);
		ActionListener toReset = new ResetToReset();
		reset.addActionListener(toReset);

		exit = new JMenuItem("Exit");
		file.add(exit);
		ActionListener toExit = new ExitToExit();
		exit.addActionListener(toExit);


		options = new JMenu("Options");
		menubar.add(options);
		Base = new JMenu("Base");

		basegroup=new ButtonGroup();
		bgroup = new ButtonGroup() ;


		options.add(Base);



		Base2 = new JRadioButton("2");
		ActionListener base2Creator = new  create2Panel();
		Base2.addActionListener(base2Creator);
		Base.add(Base2);
		basegroup.add(Base2);

		Base8 = new JRadioButton("8");
		Base.add(Base8);
		basegroup.add(Base8);
		ActionListener base8Creator = new  create8Panel();
		Base8.addActionListener(base8Creator);

		Base10 = new JRadioButton("10");
		Base.add(Base10);
		basegroup.add(Base10);
		ActionListener base10Creator = new  create10Panel();
		Base10.addActionListener(base10Creator);
		Base10.setSelected(true);

		Base16 = new JRadioButton("16");
		Base.add(Base16);
		basegroup.add(Base16);
		ActionListener base16Creator = new  create16Panel();
		Base16.addActionListener(base16Creator);

		choice = new JMenu("Choice");
		options.add(choice);

		degree = new JRadioButton("Degree");
		choice.add(degree);
		bgroup.add(degree);
		degree.setSelected(true);

		radian = new JRadioButton("Radian");
		choice.add(radian);
		bgroup.add(radian);


		help = new JMenu("Help");
		menubar.add(help);
		about = new JMenuItem("About");
		ActionListener toShow = new AboutToInfo();
		about.addActionListener(toShow);
		help.add(about);
		setJMenuBar(menubar);


		new create10Panel().actionPerformed(null);

		display = new JLabel("0");
		add(display, BorderLayout.NORTH);
		lastValue = 0;
		lastOperator = "=";
		startNewValue = true;
		setSize(FRAME_WIDTH, FRAME_HEIGHT);
		JOptionPane.showMessageDialog(null, errormsg,title, 0);

	}

	/**
      Creates the control panel with the text field 
      and buttons on the frame.
	 */

	class create2Panel implements ActionListener{

		public void actionPerformed(ActionEvent args){


			buttonPanel.removeAll();

			buttonPanel.setLayout(new GridLayout(4,4) );
			buttonPanel.add(makeDigitButton("1"));
			buttonPanel.add(makeDigitButton("0"));

			buttonPanel.add(makeOperatorButton("+"));
			buttonPanel.add(makeOperatorButton("-"));
			buttonPanel.add(makeOperatorButton("*"));
			buttonPanel.add(makeOperatorButton("/"));
			buttonPanel.add(makeOperatorButton("="));
			add(buttonPanel, BorderLayout.CENTER);

			String binaryVal = Integer.toBinaryString((int) lastValue);
			lastValue = Integer.parseInt(binaryVal);
			display.setText(""+ lastValue);
			setSize(400,350);

		}
	}

	class create8Panel implements ActionListener{

		public void actionPerformed(ActionEvent args){

			buttonPanel.removeAll();
			setSize(400,400);
			buttonPanel.setLayout(new GridLayout(4, 4) );
			buttonPanel.add(makeDigitButton("7"));
			buttonPanel.add(makeDigitButton("6"));
			buttonPanel.add(makeDigitButton("5"));
			buttonPanel.add(makeDigitButton("4"));
			buttonPanel.add(makeDigitButton("3"));
			buttonPanel.add(makeDigitButton("2"));
			buttonPanel.add(makeDigitButton("1"));
			buttonPanel.add(makeDigitButton("0"));

			buttonPanel.add(makeOperatorButton("+"));
			buttonPanel.add(makeOperatorButton("-"));
			buttonPanel.add(makeOperatorButton("*"));
			buttonPanel.add(makeOperatorButton("/"));
			buttonPanel.add(makeOperatorButton("="));

			add(buttonPanel, BorderLayout.CENTER);

			String octalVal = Integer.toOctalString((int) lastValue);
			lastValue = Integer.parseInt(octalVal);
			display.setText(""+ lastValue);

		}
	}
	class create10Panel implements ActionListener{
		public void actionPerformed (ActionEvent args){
			buttonPanel.removeAll();
			setSize(400,300);
			buttonPanel.setLayout(new GridLayout(4, 4));
			buttonPanel.add(makeDigitButton("9"));
			buttonPanel.add(makeDigitButton("8"));
			buttonPanel.add(makeDigitButton("7"));
			buttonPanel.add(makeDigitButton("6"));
			buttonPanel.add(makeDigitButton("5"));
			buttonPanel.add(makeDigitButton("4"));
			buttonPanel.add(makeDigitButton("3"));
			buttonPanel.add(makeDigitButton("2"));
			buttonPanel.add(makeDigitButton("1"));
			buttonPanel.add(makeDigitButton("0"));

			buttonPanel.add(makeOperatorButton("+"));
			buttonPanel.add(makeOperatorButton("-"));
			buttonPanel.add(makeOperatorButton("*"));
			buttonPanel.add(makeOperatorButton("/"));
			buttonPanel.add(makeOperatorButton("="));
			buttonPanel.add(makeOperatorButton("sin"));
			buttonPanel.add(makeOperatorButton("cos"));
			buttonPanel.add(makeOperatorButton("tan"));
			add(buttonPanel, BorderLayout.CENTER);

		}

	}
	class create16Panel implements ActionListener{
		public void actionPerformed (ActionEvent args){
			buttonPanel.removeAll();
			setSize(500,400);
			buttonPanel.setLayout(new GridLayout(4, 4));


			buttonPanel.add(makeDigitButton("A"));
			buttonPanel.add(makeDigitButton("B"));
			buttonPanel.add(makeDigitButton("C"));
			buttonPanel.add(makeDigitButton("D"));
			buttonPanel.add(makeDigitButton("E"));
			buttonPanel.add(makeDigitButton("F"));
			buttonPanel.add(makeDigitButton("9"));
			buttonPanel.add(makeDigitButton("8"));
			buttonPanel.add(makeDigitButton("7"));
			buttonPanel.add(makeDigitButton("6"));
			buttonPanel.add(makeDigitButton("5"));
			buttonPanel.add(makeDigitButton("4"));
			buttonPanel.add(makeDigitButton("3"));
			buttonPanel.add(makeDigitButton("2"));
			buttonPanel.add(makeDigitButton("1"));
			buttonPanel.add(makeDigitButton("0"));

			buttonPanel.add(makeOperatorButton("+"));
			buttonPanel.add(makeOperatorButton("-"));
			buttonPanel.add(makeOperatorButton("*"));
			buttonPanel.add(makeOperatorButton("/"));
			buttonPanel.add(makeOperatorButton("="));
			add(buttonPanel, BorderLayout.CENTER);
			String hexVal = Integer.toHexString((int) lastValue);
			lastValue = Integer.parseInt(hexVal);
			display.setText(""+ lastValue);

		}

	}

	class ResetToReset implements ActionListener {

		public void actionPerformed(ActionEvent args){

			lastValue = 0.0;
			display.setText("0.0");
			Base10.setSelected(true);
			new create10Panel().actionPerformed(args);
			degree.setSelected(true);
			startNewValue = true;
		}

	} 

	class ExitToExit implements ActionListener{
		public void actionPerformed(ActionEvent event){

			System.exit(0);

		}

	}

	class AboutToInfo implements ActionListener{

		private Object message = "BINGHAMTON UNIVERSITY" +"\n"+"Calculator Project"
				+ "\n" +"Ufuk BUGDAY" + "\n"+ "B00451119"; 

		public void actionPerformed(ActionEvent args0){

			JOptionPane.showMessageDialog(null, message, title, 0);

		}   

	}

	/**
      Combines two values with an operator.
      @param value1 the first value
      @param value2 the second value
      @param op an operator (+, -, *, /, or 
	 */
	public double calculate(double value1, double value2, String op)
	{  
		if (op.equals("+")) 
		{
			return value1 + value2;
		}
		else if (op.equals("-")) 
		{
			return value1 + (-value2);
		}
		else if (op.equals("*")) 
		{
			return value1 * value2;
		}
		else if (op.equals("/")) 
		{
			return value1 / value2;
		}
		if (op.equals("sin"))
		{
			return Math.sin(value1);
		}
		else if (op.equals("cos"))
		{
			return Math.cos(value1);
		}
		else if (op.equals("tan"))
		{
			return Math.tan(value1);
		}
		else // "="
		{
			return value2;
		}
	}




	class DigitButtonListener implements ActionListener
	{
		private String digit;

		/**
         Constructs a listener whose actionPerformed method adds a digit
         to the display.
         @param aDigit the digit to add
		 */
		public DigitButtonListener(String aDigit)
		{
			digit = aDigit;
		}

		public void actionPerformed(ActionEvent event)
		{  
			if (startNewValue) 
			{
				display.setText("");
				startNewValue = false;
			}
			display.setText(display.getText() + digit);
		}       
	}
	/**
      Makes a button representing a digit of a calculator.
      @param digit the digit of the calculator
      @return the button of the calculator
	 */
	public JButton makeDigitButton(String digit)
	{
		JButton button = new JButton(digit);      
		ActionListener listener = new DigitButtonListener(digit);
		button.addActionListener(listener);  
		return button;    
	} 
	class OperatorButtonListener implements ActionListener
	{
		private String operator;
		/**
         Constructs a listener whose actionPerformed method
         schedules an operator for execution.
		 */      
		public OperatorButtonListener(String anOperator)
		{
			operator = anOperator;
		}
		public void actionPerformed(ActionEvent event)
		{  
			if (!startNewValue)
			{  

				if(Base2.isSelected()){
					value=(double)Integer.parseInt(display.getText(), 2);
					lastValue = calculate(lastValue, value, lastOperator);
					display.setText("" + Integer.toString((int)lastValue, 2));

				}

				if(Base8.isSelected()){
					value=(double)Integer.parseInt(display.getText(), 8);
					lastValue = calculate(lastValue, value, lastOperator);
					display.setText("" + Integer.toString((int)lastValue, 8));
				}
				if(Base10.isSelected()){
					value = Double.parseDouble(display.getText());
					if(operator.equals("sin") || operator.equals("cos") || operator.equals("tan"))
					{
						if (!(radian.isSelected()))
							value = Math.toRadians(value);
						lastValue = calculate(value, 0, operator);
					}
					else
						lastValue = calculate(lastValue, value, lastOperator);
					display.setText("" + lastValue);
				}

				if(Base16.isSelected()){
					value=(double)Integer.parseInt(display.getText(), 16);
					lastValue = calculate(lastValue, value, lastOperator);
					display.setText("" + Integer.toString((int)lastValue, 16));
				}

				startNewValue = true;
			}

			lastOperator = operator;
		}       
	}
	/**
      Makes a button representing an operator of a calculator.
      @param op the operator of the calculator
      @return the button of the calculator
	 */
	public JButton makeOperatorButton(String op)
	{
		JButton button = new JButton(op);      
		ActionListener listener = new OperatorButtonListener(op);
		button.addActionListener(listener);  
		return button;    
	}     
}