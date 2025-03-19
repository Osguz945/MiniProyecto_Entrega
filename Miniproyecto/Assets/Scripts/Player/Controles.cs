using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public CangrejoInputs controles;
    public Vector2 direccion;
    public Rigidbody2D rb2d;
    public float velocidadMovimiento;
    public bool mirandoDerecha = true;
    public float fuerzadesalto;
    public LayerMask queEsSuelo;
    public Transform controladorDeSuelo;
    public Vector3 caja;
    public bool enSuelo=true;
    private void Awake()
    {
        controles = new();
    }
    private void OnEnable()
    {
        controles.Enable();
        controles.Cangrejo.Salto.started += _ => Saltar();
    }
    private void OnDisable()
    {
        controles.Disable();
        controles.Cangrejo.Salto.started -= _ => Saltar();
    }
    private void Update()
    {
        direccion = controles.Cangrejo.Movimiento.ReadValue<Vector2>();
        enSuelo = Physics2D.OverlapBox(controladorDeSuelo.position, caja, 0f, queEsSuelo);  
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(direccion.x * velocidadMovimiento, rb2d.velocity.y);
    }
    private void AjustraRotacion(float direccionX)
    {
        if (direccionX > 0 && !mirandoDerecha)
        {
            Girar();
            
        }
        else if (direccionX < 0 && mirandoDerecha)
        {
            Girar();
        }
       
    }
    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector2 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    private void Saltar()
    {
        if(enSuelo)
        {
            rb2d.AddForce(new Vector2 (0,fuerzadesalto), ForceMode2D.Impulse);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorDeSuelo.position, caja);    
    }

}
