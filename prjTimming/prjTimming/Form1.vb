Imports System.Drawing
Imports System.Windows.Forms
Public Class Form1
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        ' Actualiser l'horloge à chaque intervalle du timer
        PictureBox1.Invalidate()
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles PictureBox1.Paint
        ' Créez un objet Graphics à partir de l'objet PaintEventArgs
        Dim g As Graphics = e.Graphics

        ' Centre du cercle
        Dim centerX As Integer = PictureBox1.Width \ 2
        Dim centerY As Integer = PictureBox1.Height \ 2

        ' Rayon du cercle
        Dim radius As Integer = Math.Min(centerX, centerY) - 10

        ' Dessiner le cercle de l'horloge
        g.DrawEllipse(Pens.Black, centerX - radius, centerY - radius, 2 * radius, 2 * radius)

        ' Obtenir l'heure actuelle
        Dim now As DateTime = DateTime.Now
        Dim hours As Integer = now.Hour Mod 12
        Dim minutes As Integer = now.Minute
        Dim seconds As Integer = now.Second

        ' Dessiner les aiguilles
        DrawClockHand(g, centerX, centerY, hours * (360 / 12) + (minutes / 60) * (360 / 12), radius * 0.5, 6)
        DrawClockHand(g, centerX, centerY, minutes * (360 / 60) + (seconds / 60) * (360 / 60), radius * 0.7, 4)
        DrawClockHand(g, centerX, centerY, seconds * (360 / 60), radius * 0.9, 2)

        ' Dessiner les marques pour chaque heure
        For i As Integer = 0 To 11
            Dim angle As Double = i * (360 / 12)
            
            Dim startX As Integer = centerX + CInt(radius * Math.Cos(angle * (Math.PI / 180)))
            Dim startY As Integer = centerY + CInt(radius * Math.Sin(angle * (Math.PI / 180)))
            Dim endX As Integer = centerX + CInt((radius - 10) * Math.Cos(angle * (Math.PI / 180)))
            Dim endY As Integer = centerY + CInt((radius - 10) * Math.Sin(angle * (Math.PI / 180)))

            g.DrawLine(Pens.Black, startX, startY, endX, endY)
        Next
    End Sub

    Private Sub DrawClockHand(ByVal g As Graphics, ByVal x As Integer, ByVal y As Integer, ByVal angle As Double, ByVal length As Double, ByVal lineWidth As Integer)
        Dim endX As Integer = x + CInt(length * Math.Cos(angle * (Math.PI / 180)))
        Dim endY As Integer = y + CInt(length * Math.Sin(angle * (Math.PI / 180)))

        g.DrawLine(New Pen(Color.Black, lineWidth), x, y, endX, endY)
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Démarrez le Timer lorsque le formulaire est chargé
        Timer1.Start()
        BackColor = Color.White
    End Sub
End Class
