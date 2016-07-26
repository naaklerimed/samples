import javax.swing.JFrame;
public class Viewer {

	public static void main(String[] args)
	{ 
		CountrySelector cFrame = new CountrySelector();
		cFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		cFrame.setTitle("Country Information");

		cFrame.setVisible(true);
	}

}



